using System.Text;
using System.Threading.RateLimiting;
using Asp.Versioning;
using MadWorld.Backend.API.Endpoints;
using MadWorld.Backend.Application.CommonLogic.Extensions;
using MadWorld.Backend.Domain.Options;
using MadWorld.Backend.Infrastructure.Database.Extensions;
using MadWorld.Shared.Infrastructure.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;

namespace MadWorld.Backend.API;

public sealed class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Host.UseSerilog((context, configuration) =>
            configuration.ReadFrom.Configuration(context.Configuration));

        builder.Services.AddOptions<GrpcSettings>()
                        .Bind(builder.Configuration
                            .GetSection(GrpcSettings.SectionName)
                        );

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(opt =>
        {
            opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MadWorld API", Version = "v1" });
            opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "bearer"
            });
            opt.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });
        });

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudiences = builder.Configuration.GetSection("Jwt:Audiences").Get<string[]>(),
                IssuerSigningKey =
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
            };
        });
        builder.Services.AddAuthorization();

        builder.Services.AddHealthChecks();

        builder.AddApplication();
        builder.AddDatabase();

        builder.Services.Configure<ForwardedHeadersOptions>(options =>
        {
            options.ForwardedHeaders =
                ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
        });
        
        const string madWorldOrigins = "_myAllowSpecificOrigins";
        builder.Services.AddCors(options =>
        {
            options.AddPolicy(name: madWorldOrigins,
                policy =>
                {
                    policy.WithOrigins(builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>()!);
                    policy.AllowAnyMethod();
                    policy.AllowAnyHeader();
                });
        });

        builder.Services.AddRateLimiter(rateLimiterOptions =>
            {
                rateLimiterOptions.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

                rateLimiterOptions.AddPolicy(RateLimiterNames.GeneralLimiter, httpContext =>
                {
                    return RateLimitPartition.GetFixedWindowLimiter(
                        partitionKey: httpContext.Request.Headers["X-Forwarded-For"],
                        factory: _ => new FixedWindowRateLimiterOptions()
                        {
                            PermitLimit = 10,
                            Window = TimeSpan.FromSeconds(10)
                        });
                });
            }
        );
        
        builder.Services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
            options.ApiVersionReader = new HeaderApiVersionReader("api-version");
        });

        var app = builder.Build();
        app.UseForwardedHeaders();

        app.UseSwagger();
        app.UseSwaggerUI();
        app.MapHealthChecks("/healthz");

        app.UseAuthentication();
        app.UseAuthorization();
        
        var apiBuilder = app.NewVersionedApi()
            .HasApiVersion(1, 0)
            .ReportApiVersions();

        apiBuilder.AddCurriculumVitaeEndpoints();
        apiBuilder.AddTestEndpoints();

        app.UseRateLimiter();
        app.UseCors(madWorldOrigins);

        app.MigrateDatabases();

        app.Run();
    }
}
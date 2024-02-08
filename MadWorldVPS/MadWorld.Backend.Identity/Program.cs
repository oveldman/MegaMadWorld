using System.Text;
using System.Threading.RateLimiting;
using MadWorld.Backend.Identity.Application;
using MadWorld.Backend.Identity.BackgroundServices;
using MadWorld.Backend.Identity.Domain;
using MadWorld.Backend.Identity.Domain.Users;
using MadWorld.Backend.Identity.Endpoints;
using MadWorld.Backend.Identity.Infrastructure;
using MadWorld.Shared.Common.Extensions;
using MadWorld.Shared.Infrastructure.Databases;
using MadWorld.Shared.Infrastructure.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;

namespace MadWorld.Backend.Identity;

public sealed class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Host.UseSerilog((context, configuration) =>
            configuration.ReadFrom.Configuration(context.Configuration));

        builder.AddCommon();
        
        builder.Services.AddSingleton<IEmailSender<IdentityUserExtended>, EmailSender>();
        builder.Services.AddScoped<DeleteExpiredSessionsUseCase>();
        builder.Services.AddScoped<DeleteSessionUseCase>();
        builder.Services.AddScoped<GetUsersUseCase>();
        builder.Services.AddScoped<GetUserUseCase>();
        builder.Services.AddScoped<PatchUserUseCase>();
        builder.Services.AddScoped<GetRolesUseCase>();
        builder.Services.AddScoped<PostJwtLoginUseCase>();
        builder.Services.AddScoped<PostJwtRefreshUseCase>();

        builder.Services.AddScoped<IJwtGenerator, JwtGenerator>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();

        builder.Services.AddHostedService<DeleteSessionService>();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(opt =>
        {
            opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MadWorld Identity", Version = "v1" });
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

        builder.Services.AddDbContext<UserDbContext>(
            options =>
                options.UseNpgsql(builder.BuildConnectionString("IdentityConnectionString")));

        builder.Services.AddIdentityApiEndpoints<IdentityUserExtended>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<UserDbContext>();

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
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
            };
        });

        builder.Services.AddAuthorizationBuilder()
            .AddPolicy(Policies.IdentityAdministrator, policy =>
                policy.RequireRole(Roles.IdentityAdministrator));

        builder.Services.AddAuthorization();

        builder.Services.AddHealthChecks();

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

        var app = builder.Build();
        app.UseForwardedHeaders();

        app.UseSwagger();
        app.UseSwaggerUI();
        app.MapHealthChecks("/healthz");

        app.UseAuthentication();
        app.UseAuthorization();

        app.AddIdentityEndpoints();
        app.AddUserManagerEndpoints();

        app.UseRateLimiter();
        app.UseCors(madWorldOrigins);

        app.MigrateDatabase<UserDbContext>();
        await app.AddFirstAdminAccountAsync();

        app.Run();
    }
}

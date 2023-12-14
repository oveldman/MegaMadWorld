using System.Text;
using System.Threading.RateLimiting;
using MadWorld.Shared.Infrastructure.Databases;
using MadWorld.Shared.Infrastructure.Settings;
using MadWorld.ShipSimulator.API.Endpoints;
using MadWorld.ShipSimulator.Application.CommonLogic.Extensions;
using MadWorld.ShipSimulator.Infrastructure.Database;
using MadWorld.ShipSimulator.Infrastructure.Database.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((context, configuration) => 
    configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MadWorld Ship Simulator", Version = "v1" });
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
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

builder.AddApplication();
builder.AddDatabase();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
    };
});

builder.Services.AddAuthorizationBuilder()
    .AddPolicy(Policies.IdentityShipSimulator, policy =>
        policy.RequireRole(Roles.IdentityShipSimulator));

builder.Services.AddAuthorization();

builder.Services.AddHealthChecks();

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders =
        ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
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

app.UseRateLimiter();

app.AddDangerEndpoints();

app.MigrateDatabase<ShipSimulatorContext>();

app.Run();
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using MadWorld.Backend.Identity.Contracts;
using MadWorld.Shared.Infrastructure.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace MadWorld.Backend.Identity.Endpoints;

public static class IdentityEndpoints
{
    public static void AddIdentityEndpoints(this WebApplication app)
    {
        var account = app.MapGroup("/Account")
            .WithTags("Account")
            .RequireRateLimiting(RateLimiterNames.GeneralLimiter);

        account.MapIdentityApi<IdentityUser>()
            .WithOpenApi();
        
        account.MapPost("/JwtLogin", 
                async ([FromBody] JwtLoginRequest request, 
                [FromServices] SignInManager<IdentityUser> signInManager,
                [FromServices] UserManager<IdentityUser> userManager) =>
            {
                    var result = await signInManager.PasswordSignInAsync(request.Email, request.Password, false, false);
                    if (!result.Succeeded)
                    {
                        return Results.Unauthorized();
                    }

                    var user = await userManager.FindByEmailAsync(request.Email);
                    var roles = await userManager.GetRolesAsync(user!);
                    
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes(app.Configuration["Jwt:Key"]!);

                    var claims = new List<Claim>
                    {
                        new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new(ClaimTypes.NameIdentifier, user!.Id),
                        new(JwtRegisteredClaimNames.Sub, user.Email!),
                        new(JwtRegisteredClaimNames.Email, user.Email!),
                    };

                    if (roles.Any())
                    {
                        claims.AddRange(
                            roles.Select(role => 
                                new Claim(ClaimTypes.Role, role)));
                    }

                    var expires = DateTime.UtcNow.AddHours(1);
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(claims),
                        Expires = expires,
                        Issuer = app.Configuration["Jwt:Issuer"],
                        Audience = app.Configuration["Jwt:Audience"],
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    };
                    
                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    var jwt = tokenHandler.WriteToken(token)!;
                    var refreshToken = GenerateRefreshToken();

                    await signInManager.RefreshSignInAsync(user);
                    
                    return Results.Ok(new JwtLoginResponse
                    {
                        IsSuccess = true,
                        Jwt = jwt,
                        Expires = expires,
                        RefreshToken = refreshToken
                    });
                })
            .WithName("JwtLogin")
            .WithOpenApi()
            .AllowAnonymous();
    }
    
    private static string GenerateRefreshToken()
    {
        var randomNumber = new byte[512];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
}
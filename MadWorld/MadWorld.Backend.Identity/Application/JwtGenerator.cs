using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MadWorld.Backend.Identity.Domain;
using MadWorld.Backend.Identity.Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace MadWorld.Backend.Identity.Application;

public class JwtGenerator : IJwtGenerator
{
    private readonly IConfiguration _configuration;

    public JwtGenerator(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public JwtToken GenerateToken(IdentityUserExtended user, IList<string> roles)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]!);

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

        var expires = DateTime.UtcNow.AddMinutes(15);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = expires,
            Issuer = _configuration["Jwt:Issuer"],
            Audience = _configuration["Jwt:Audience"],
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var jwt = tokenHandler.WriteToken(token)!;

        return new JwtToken()
        {
            Token = jwt,
            Expired = expires
        };
    }
}
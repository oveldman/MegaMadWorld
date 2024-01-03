using System.Security.Cryptography;
using MadWorld.Backend.Identity.Contracts;
using MadWorld.Backend.Identity.Domain;
using MadWorld.Backend.Identity.Domain.Users;
using MadWorld.Backend.Identity.Infrastructure;
using Microsoft.AspNetCore.Identity;

namespace MadWorld.Backend.Identity.Application;

public class PostJwtLoginUseCase
{
    private readonly IJwtGenerator _jwtGenerator;
    private readonly IUserRepository _userRepository;
    private readonly SignInManager<IdentityUserExtended> _signInManager;
    private readonly UserManager<IdentityUserExtended> _userManager;

    public PostJwtLoginUseCase(IJwtGenerator jwtGenerator, IUserRepository userRepository, SignInManager<IdentityUserExtended> signInManager, UserManager<IdentityUserExtended> userManager)
    {
        _jwtGenerator = jwtGenerator;
        _userRepository = userRepository;
        _signInManager = signInManager;
        _userManager = userManager;
    }
    
    public async Task<IResult> PostJwtLogin(JwtLoginRequest request)
    {
        var result = await _signInManager.PasswordSignInAsync(request.Email, request.Password, false, false);
        if (!result.Succeeded)
        {
            return Results.Unauthorized();
        }

        var user = await _userManager.FindByEmailAsync(request.Email);
        var roles = await _userManager.GetRolesAsync(user!);
        
        var jwt = _jwtGenerator.GenerateToken(user!, roles);
        var token = GenerateRefreshToken();

        var refreshToken = new RefreshToken(token, expires: DateTime.UtcNow.AddDays(7), user!.Id);
        await _userRepository.AddRefreshToken(refreshToken);
        
        return Results.Ok(new JwtLoginResponse
        {
            IsSuccess = true,
            Jwt = jwt.Token,
            Expires = jwt.Expired,
            RefreshToken = token
        });
    }
    
    private static string GenerateRefreshToken()
    {
        var randomNumber = new byte[512];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
}
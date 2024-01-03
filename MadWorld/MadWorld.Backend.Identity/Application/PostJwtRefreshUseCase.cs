using MadWorld.Backend.Identity.Contracts;
using MadWorld.Backend.Identity.Domain;
using MadWorld.Backend.Identity.Domain.Users;
using MadWorld.Backend.Identity.Infrastructure;
using Microsoft.AspNetCore.Identity;

namespace MadWorld.Backend.Identity.Application;

public class PostJwtRefreshUseCase
{
    private readonly IJwtGenerator _jwtGenerator;
    private readonly UserManager<IdentityUserExtended> _userManager;
    private readonly IUserRepository _userRepository;

    public PostJwtRefreshUseCase(IJwtGenerator jwtGenerator, UserManager<IdentityUserExtended> userManager, IUserRepository userRepository)
    {
        _jwtGenerator = jwtGenerator;
        _userManager = userManager;
        _userRepository = userRepository;
    }
    
    public async Task<IResult> PostJwtRefresh(JwtRefreshRequest request)
    {
        var refreshToken = _userRepository.GetRefreshToken(request.RefreshToken);

        if (refreshToken == null || refreshToken.Expires < DateTime.UtcNow)
        {
            return Results.Unauthorized();
        }

        var user = refreshToken.User;
        var roles = await _userManager.GetRolesAsync(user);
        
        var jwt = _jwtGenerator.GenerateToken(user, roles);

        return Results.Ok(new JwtRefreshResponse()
        {
            IsSuccess = true,
            Jwt = jwt.Token,
            Expires = jwt.Expired,
            RefreshToken = request.RefreshToken
        });
    }
}
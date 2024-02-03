using MadWorld.Backend.Identity.Contracts.UserManagers;
using MadWorld.Backend.Identity.Domain.Users;

namespace MadWorld.Backend.Identity.Application.Mappers;

public static class IdentityUserMapper
{
    public static List<UserContract> ToDto(this List<IdentityUserExtended> users)
    {
        return users
            .Select(ToUserContract)
            .ToList();
    }
    
    public static GetUserResponse ToDto(this IdentityUserExtended user, IEnumerable<string> roles)
    {
        return new GetUserResponse()
        {
            Id = user.Id,
            Email = user.Email!,
            IsBlocked = user.LockoutEnabled,
            Roles = roles.ToList(),
            RefreshTokens = user.RefreshTokens
                .Select(ToRefreshTokenContract)
                .ToList()
        };
    }

    private static RefreshTokenContract ToRefreshTokenContract(RefreshToken refreshToken)
    {
        return new RefreshTokenContract()
        {
            Id = refreshToken.Id.ToString(),
            Audience = refreshToken.Audience,
            Expires = refreshToken.Expires
        };
    }

    private static UserContract ToUserContract(IdentityUserExtended user)
    {
        return new UserContract
        {
            Id = user.Id,
            Email = user.Email!,
            IsBlocked = user.LockoutEnabled,
        };
    }
}
using MadWorld.Backend.Identity.Contracts.UserManagers;
using Microsoft.AspNetCore.Identity;

namespace MadWorld.Backend.Identity.Application.Mappers;

public static class IdentityUserMapper
{
    public static List<UserContract> ToDto(this List<IdentityUser> users)
    {
        return users
            .Select(ToUserContract)
            .ToList();
    }
    
    public static GetUserResponse ToDto(this IdentityUser user, IEnumerable<string> roles)
    {
        return new GetUserResponse()
        {
            Id = user.Id,
            Email = user.Email!,
            Roles = roles.ToList()
        };
    }

    private static UserContract ToUserContract(IdentityUser user)
    {
        return new UserContract
        {
            Id = user.Id,
            Email = user.Email!
        };
    }
}
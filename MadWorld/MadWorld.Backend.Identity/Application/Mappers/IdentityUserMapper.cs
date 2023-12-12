using MadWorld.Backend.Identity.Endpoints;
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

    private static UserContract ToUserContract(IdentityUser user)
    {
        return new UserContract()
        {

        };
    }
}
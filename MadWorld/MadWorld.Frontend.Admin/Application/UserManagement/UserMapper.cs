using MadWorld.Backend.Identity.Contracts.UserManagers;

namespace MadWorld.Frontend.Admin.Application.UserManagement;

public static class UserMapper
{
    public static UserDetails ToDetails(this GetUserResponse response)
    {
        return new UserDetails
        {
            Id = response.Id,
            Email = response.Email,
            Roles = response.Roles,
            IsBlocked = response.IsBlocked,
            RefreshTokens = response.RefreshTokens
                                .Select(ToDetails)
                                .ToList()
        };
    }

    public static PatchUserRequest ToRequest(this UserDetails details)
    {
        return new PatchUserRequest
        {
            Id = details.Id,
            Roles = details.Roles,
            IsBlocked = details.IsBlocked
        };
    }
    
    private static RefreshTokenDetails ToDetails(this RefreshTokenContract contract)
    {
        return new RefreshTokenDetails
        {
            Id = contract.Id,
            Expires = contract.Expires
        };
    }
}
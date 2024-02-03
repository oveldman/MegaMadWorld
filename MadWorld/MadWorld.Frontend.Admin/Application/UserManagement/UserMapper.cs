using MadWorld.Backend.Identity.Contracts.UserManagers;

namespace MadWorld.Frontend.Admin.Application.UserManagement;

public static class UserMapper
{
    public static UserDetails ToDetails(this GetUserResponse response, IReadOnlyList<string> allRoles)
    {
        return new UserDetails
        {
            Id = response.Id,
            Email = response.Email,
            Roles = ToDetails(allRoles, response.Roles),
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
            Roles = details.Roles.ToRequest(),
            IsBlocked = details.IsBlocked
        };
    }
    
    private static List<RolesDetails> ToDetails(IEnumerable<string> allRoles, IReadOnlyList<string> activeRoles)
    {
        return allRoles
            .Select(role => new RolesDetails
            {
                Name = role,
                IsActive = activeRoles.Contains(role)
            })
            .ToList();
    }

    private static RefreshTokenDetails ToDetails(this RefreshTokenContract contract)
    {
        return new RefreshTokenDetails
        {
            Id = contract.Id,
            Expires = contract.Expires
        };
    }
    
    private static List<string> ToRequest(this IEnumerable<RolesDetails> roles)
    {
        return roles
            .Where(role => role.IsActive)
            .Select(role => role.Name)
            .ToList();
    }
}
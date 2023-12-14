using MadWorld.Backend.Identity.Endpoints;
using Microsoft.AspNetCore.Identity;

namespace MadWorld.Backend.Identity.Application;

public class GetRolesUseCase
{
    private readonly RoleManager<IdentityRole> _manager;

    public GetRolesUseCase(RoleManager<IdentityRole> manager)
    {
        _manager = manager;
    }
    
    public GetRolesResponse GetRoles()
    {
        var roles = _manager.Roles
            .Select(x => x.Name!)
            .ToList();
        
        return new GetRolesResponse()
        {
            Roles = roles
        };
    }
}
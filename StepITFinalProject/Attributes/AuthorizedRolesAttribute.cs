using Microsoft.AspNetCore.Authorization;
using StepITFinalProject.Enums;

namespace StepITFinalProject.Attributes;

public class AuthorizedRolesAttribute : AuthorizeAttribute
{
    public AuthorizedRolesAttribute(params ROLES[] roles)
    {
        Roles = string.Join(",", roles.Select(r => r.ToString()));
    }
}

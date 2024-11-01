using fsacwapi.Core.Enums;
using Microsoft.AspNetCore.Authorization;

namespace fsacwapi.Infrastructure.Attributes;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
public class AuthorizeRoleAttribute : AuthorizeAttribute
{
    public AuthorizeRoleAttribute(Role role) : base()
    {
        Roles = role.ToString();
    }
}
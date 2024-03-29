using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace FITApp.Auth.Attributes;

/// <summary>
/// Forbids access to the decorated controller or action if the user does not have any of the specified permissions. This attribute should be used placed after the AuthorizeAttribute since this attribute assumes that the user is already authenticated.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
public class RequiresPermissionAttribute : Attribute, IAuthorizationFilter
{
    private const string ExpectedClaimValue = "true";
    private readonly string[] _permissions;

    /// <summary>
    /// Initializes a new instance of the <see cref="RequiresPermissionAttribute"/> class.
    /// </summary>
    /// <param name="permissions">The permissions required to access the decorated controller or action. If the user has any of these permissions as a claim with a value of "true", access is granted.</param>
    public RequiresPermissionAttribute(params string[] permissions)
    {
        _permissions = permissions;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var user = context.HttpContext.User;
        var claims = user.Claims;
        var hasPermission = false;
        foreach (var permission in _permissions)
        {
            if (claims.Any(c => c.Type == permission && c.Value == ExpectedClaimValue))
            {
                hasPermission = true;
                break;
            }
        }

        if (!hasPermission)
        {
            context.Result = new ForbidResult();
        }
    }
}
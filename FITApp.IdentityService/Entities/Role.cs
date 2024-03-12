using Microsoft.AspNetCore.Identity;

namespace FITApp.IdentityService.Entities;

public class Role : IdentityRole
{
    public bool IsAssignable { get; set; }
}
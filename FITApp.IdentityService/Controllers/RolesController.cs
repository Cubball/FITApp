using Microsoft.AspNetCore.Mvc;
using FITApp.IdentityService.Contracts.Responses;
using FITApp.IdentityService.Contracts.Requests;
using Microsoft.AspNetCore.Identity;
using FITApp.IdentityService.Entities;
using System.Security.Claims;
using FITApp.IdentityService.Data;

namespace FITApp.IdentityService.Controllers;

// NOTE: this controller will require authorazation
[ApiController]
[Route("api/[controller]")]
public class RolesController : ControllerBase
{
    private readonly RoleManager<Role> _roleManager;
    private readonly AppDbContext _context;
    private readonly UserManager<User> _userManager;

    public RolesController(RoleManager<Role> roleManager, AppDbContext context, UserManager<User> userManager)
    {
        _roleManager = roleManager;
        _context = context;
        _userManager = userManager;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<FullRoleResponse>> Get(string id)
    {
        var role = await _roleManager.FindByIdAsync(id);
        if (role == null)
        {
            return NotFound();
        }

        var claims = (await _roleManager.GetClaimsAsync(role)).Select(c => c.Type);

        return Ok(new FullRoleResponse
        {
            Id = role.Id,
            Name = role.Name,
            Permissions = claims
        });
    }

    [HttpGet]
    public ActionResult<IEnumerable<ShortRoleResponse>> Get()
    {
        var roles = _roleManager.Roles.ToList();
        var result = roles.Select(r => new ShortRoleResponse
        {
            Id = r.Id,
            Name = r.Name
        });

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] CreateRoleRequest request)
    {
        var permissions = _context.Permissions.Select(p => p.Name).ToList();

        foreach (var permission in request.Permissions)
        {
            if (!permissions.Contains(permission))
            {
                return BadRequest("Permission does not exist");
            }
        }

        var role = new Role
        {
            Name = request.Name,
            IsAssignable = true
        };
        await _roleManager.CreateAsync(role);

        foreach (var permission in request.Permissions)
        {
            await _roleManager.AddClaimAsync(role, new Claim(permission, "true"));
        }

        return Created();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(string id)
    {
        var role = await _roleManager.FindByIdAsync(id);
        if (role == null)
        {
            return NotFound();
        }

        var users = await _userManager.GetUsersInRoleAsync(role.Name!);

        if (users.Count > 0)
        {
            return BadRequest("Role is not empty");
        }

        await _roleManager.DeleteAsync(role);

        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(string id, [FromBody] UpdateRoleRequest request)
    {
        var permissions = _context.Permissions.Select(p => p.Name).ToList();

        foreach (var permission in request.Permissions)
        {
            if (!permissions.Contains(permission))
            {
                return BadRequest("Permission does not exist");
            }
        }

        var role = await _roleManager.FindByIdAsync(id);
        if (role == null)
        {
            return NotFound();
        }

        role.Name = request.Name;

        await _roleManager.UpdateAsync(role);
        var claims = await _roleManager.GetClaimsAsync(role);
        foreach (var claim in claims)
        {
            await _roleManager.RemoveClaimAsync(role, claim);
        }

        foreach (var permission in request.Permissions)
        {
            await _roleManager.AddClaimAsync(role, new Claim(permission, "true"));
        }

        return Ok();
    }

    [HttpGet("/permissions")]
    public ActionResult<IEnumerable<Permission>> GetPermissions()
    {
        var permissions = _context.Permissions.ToList();
        return Ok(permissions);
    }
}
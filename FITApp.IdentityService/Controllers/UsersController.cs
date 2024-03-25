using FITApp.Auth.Attributes;
using FITApp.Auth.Data;
using FITApp.IdentityService.Contracts.Responses;
using FITApp.IdentityService.Contracts.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FITApp.IdentityService.Entities;
using Microsoft.AspNetCore.Identity;
using MimeKit;
using MailKit.Security;
using FITApp.IdentityService.Services;

namespace FITApp.IdentityService.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;
    private readonly IEmailSender _emailSender;
    private readonly IPasswordGenerator _passwordGenerator;

    public UsersController(UserManager<User> userManager,
    RoleManager<Role> roleManager,
    IEmailSender emailSender,
    IPasswordGenerator passwordGenerator)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _emailSender = emailSender;
        _passwordGenerator = passwordGenerator;
    }

    [RequiresPermission(Permissions.UsersRead, Permissions.All)]
    [HttpGet("{id}")]
    public async Task<ActionResult<UserResponse>> Get(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        var roles = await _userManager.GetRolesAsync(user);
        var role = await _roleManager.FindByNameAsync(roles[0]);

        return Ok(new UserResponse
        {
            Id = user.Id,
            Email = user.Email,
            RoleId = role.Id,
            RoleName = role.Name
        });
    }

    [RequiresPermission(Permissions.UsersRead, Permissions.All)]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserResponse>>> Get()
    {
        var users = _userManager.Users.ToList();

        var result = new List<UserResponse>();

        foreach (var user in users)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var role = await _roleManager.FindByNameAsync(roles[0]);

            result.Add(new UserResponse
            {
                Id = user.Id,
                Email = user.Email,
                RoleId = role.Id,
                RoleName = role.Name
            });
        }

        return Ok(result);
    }

    [RequiresPermission(Permissions.UsersCreate, Permissions.All)]
    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CreateUserRequest userRequest)
    {
        var isRightEmail = userRequest.Email.EndsWith("@knu.ua");
        if (!isRightEmail)
        {
            return BadRequest("Email is not valid");
        }

        var userCheck = await _userManager.FindByEmailAsync(userRequest.Email);
        if (userCheck != null)
        {
            return BadRequest("User with this email already exists");
        }

        var password = _passwordGenerator.GeneratePassword();

        var user = new User
        {
            Email = userRequest.Email,
            UserName = userRequest.Email
        };

        var role = await _roleManager.FindByIdAsync(userRequest.RoleId);
        if (role == null)
        {
            return BadRequest("Role not found");
        }

        if (!role.IsAssignable)
        {
            return BadRequest("Role is not assignable");
        }

        await _userManager.CreateAsync(user, password!);
        await _userManager.AddToRoleAsync(user, role.Name!);

        var subject = "FITApp registration";
        var message = $"You have been registered in FITApp. Your password is {password}";

        await _emailSender.SendEmail(userRequest.Email, subject, message);

        return Created();
    }

    [RequiresPermission(Permissions.UsersUpdate, Permissions.All)]
    [HttpPost("{id}/reset-password")]
    public async Task<ActionResult> ResetPassword(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }
        var password = _passwordGenerator.GeneratePassword();

        var result = await _userManager.RemovePasswordAsync(user);
        if (!result.Succeeded)
        {
            return BadRequest();
        }

        result = await _userManager.AddPasswordAsync(user, password);
        if (!result.Succeeded)
        {
            return BadRequest();
        }

        user.RefreshToken = null;
        user.RefreshTokenExpiryTime = null;
        await _userManager.UpdateAsync(user);

        var subject = "FITApp password reset";
        var message = $"Your password has been reset. Your new password is {password}";

        await _emailSender.SendEmail(user.Email!, subject, message);

        return Ok();
    }

    [RequiresPermission(Permissions.UsersDelete, Permissions.All)]
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        await _userManager.DeleteAsync(user);
        return Ok();
    }

    [RequiresPermission(Permissions.UsersUpdate, Permissions.All)]
    [HttpPut("{id}/role")]
    public async Task<ActionResult> ChangeRole(string id, [FromBody] ChangeRoleRequest changeRoleRequest)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        var newRole = await _roleManager.FindByIdAsync(changeRoleRequest.RoleId);
        if (newRole == null)
        {
            return BadRequest("Role not found");
        }

        if (!newRole.IsAssignable)
        {
            return BadRequest("Role is not assignable");
        }

        var oldRole = (await _userManager.GetRolesAsync(user))[0];

        await _userManager.RemoveFromRoleAsync(user, oldRole);

        await _userManager.AddToRoleAsync(user, newRole.Name!);

        return Ok();
    }
}
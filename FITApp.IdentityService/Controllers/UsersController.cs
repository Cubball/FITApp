using FITApp.IdentityService.Contracts.Responses;
using FITApp.IdentityService.Contracts.Requests;
using Microsoft.AspNetCore.Mvc;
using FITApp.IdentityService.Entities;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using MimeKit;
using System.Net.Mail;
using MailKit.Net.Smtp;
using MailKit;
using MailKit.Security;

namespace FITApp.IdentityService.Controllers;

// NOTE: this controller will require authorazation
[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;
    private readonly IConfiguration _configuration;

    public UsersController(UserManager<User> userManager, RoleManager<Role> roleManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
    }
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

        var password = _configuration.GetSection("DefaultPassword").Value;

        var user = new User
        {
            Email = userRequest.Email,
            UserName = userRequest.Email
        };

        await _userManager.CreateAsync(user, password!);

        var role = await _roleManager.FindByIdAsync(userRequest.RoleId);

        if (role == null)
        {
            return BadRequest("Role not found");
        }

        await _userManager.AddToRoleAsync(user, role.Name!);

        var subject = "FITApp registration";
        var message = $"You have been registered in FITApp. Your password is {password}";

        SendEmail(userRequest.Email, subject, message);

        return Created();
    }

    [HttpPost("{id}/reset-password")]
    public async Task<ActionResult> ResetPassword(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }
        var password = Guid.NewGuid().ToString().Substring(0, 8);

        await _userManager.RemovePasswordAsync(user);
        await _userManager.AddPasswordAsync(user, password);

        var subject = "FITApp password reset";
        var message = $"Your password has been reset. Your new password is {password}";

        SendEmail(user.Email!, subject, message);

        return Ok();
    }

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

        var oldRole = (await _userManager.GetRolesAsync(user))[0];

        await _userManager.RemoveFromRoleAsync(user, oldRole);

        await _userManager.AddToRoleAsync(user, newRole.Name!);

        return Ok();
    }

    private void SendEmail(string email, string subject, string messageBody)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("FitApp", "FitApp@gmail.com"));
        message.To.Add(new MailboxAddress("", email));
        message.Subject = subject;
        message.Body = new TextPart("plain") { Text = messageBody };

        var senderEmail = _configuration.GetSection("EmailSettings:Email").Value;
        var senderPassword = _configuration.GetSection("EmailSettings:Password").Value;

        using var client = new MailKit.Net.Smtp.SmtpClient();
        client.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
        client.Authenticate(senderEmail, senderPassword);
        client.Send(message);
        client.Disconnect(true);
    }
}
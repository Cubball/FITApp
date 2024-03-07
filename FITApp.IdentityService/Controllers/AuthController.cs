using FITApp.IdentityService.Contracts.Requests;
using FITApp.IdentityService.Contracts.Responses;

using Microsoft.AspNetCore.Mvc;

namespace FITApp.IdentityService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    [HttpPost("login")]
    public ActionResult<TokensResponse> Login([FromBody] LoginRequest request)
    {
        // TODO:
        throw new NotImplementedException();
    }

    [HttpPost("refresh")]
    public ActionResult<TokensResponse> Refresh([FromBody] RefreshRequest request)
    {
        // TODO:
        throw new NotImplementedException();
    }

    [HttpPost("change-password")]
    public ActionResult ChangePassword([FromBody] ChangePasswordRequest request)
    {
        // TODO: this endpoint will require authentication
        throw new NotImplementedException();
    }
}
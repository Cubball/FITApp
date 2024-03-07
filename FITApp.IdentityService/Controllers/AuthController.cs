using FITApp.IdentityService.Contracts.Requests;
using FITApp.IdentityService.Contracts.Responses;

using Microsoft.AspNetCore.Mvc;

namespace FITApp.IdentityService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    [HttpPost("login")]
    public ActionResult<LoginResponse> Login([FromBody] LoginRequest request)
    {
        // TODO:
        throw new NotImplementedException();
    }

    [HttpPost("refresh")]
    public ActionResult<RefreshResponse> Refresh([FromBody] RefreshRequest request)
    {
        // TODO:
        throw new NotImplementedException();
    }
}
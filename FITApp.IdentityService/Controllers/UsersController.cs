using FITApp.IdentityService.Contracts.Responses;
using Microsoft.AspNetCore.Mvc;

namespace FITApp.IdentityService.Controllers;

// NOTE: this controller will require authorazation
[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    [HttpGet("{id}")]
    public ActionResult<UserResponse> Get(string id)
    {
        // TODO:
        throw new NotImplementedException();
    }

    [HttpGet]
    public ActionResult<IEnumerable<UserResponse>> Get()
    {
        // TODO:
        throw new NotImplementedException();
    }
}
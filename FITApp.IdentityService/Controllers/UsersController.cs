using FITApp.IdentityService.Contracts.Responses;
using FITApp.IdentityService.Contracts.Requests;
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

    [HttpPost]
    public ActionResult Create([FromBody] CreateUserRequest userRequest)
    {
        // TODO:
        throw new NotImplementedException();
    }

    [HttpPost("{id}/reset-password")]
    public ActionResult ResetPassword(string id)
    {
        // TODO:
        throw new NotImplementedException();
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(string id)
    {
        // TODO:
        throw new NotImplementedException();
    }

    [HttpPut("{id}/role")]
    public ActionResult ChangeRole(string id, [FromBody] ChangeRoleRequest changeRoleRequest)
    {
        // TODO:
        throw new NotImplementedException();
    }
}
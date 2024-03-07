using Microsoft.AspNetCore.Mvc;
using FITApp.IdentityService.Contracts.Responses;
using FITApp.IdentityService.Contracts.Requests;

namespace FITApp.IdentityService.Controllers;

// NOTE: this controller will require authorazation
[ApiController]
[Route("api/[controller]")]
public class RolesController : ControllerBase
{
    [HttpGet("{id}")]
    public ActionResult<FullRoleResponse> Get(string id)
    {
        // TODO:
        throw new NotImplementedException();
    }

    [HttpGet]
    public ActionResult<IEnumerable<ShortRoleResponse>> Get()
    {
        // TODO:
        throw new NotImplementedException();
    }

    [HttpPost]
    public ActionResult Post([FromBody] CreateRoleRequest request)
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

    [HttpPut("{id}")]
    public ActionResult Put(string id, [FromBody] UpdateRoleRequest request)
    {
        // TODO:
        throw new NotImplementedException();
    }
}
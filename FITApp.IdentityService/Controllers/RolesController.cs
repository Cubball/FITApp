using Microsoft.AspNetCore.Mvc;
using FITApp.IdentityService.Contracts.Responses;

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
}
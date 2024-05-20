using FITApp.Auth.Attributes;
using FITApp.Auth.Data;
using FITApp.EmployeesService.Dtos;
using FITApp.EmployeesService.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FITApp.EmployeesService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AdministrationController(IAdministrationService administrationService) : ControllerBase
    {
        private readonly IAdministrationService _administrationService = administrationService;

        [HttpGet]
        public async Task<ActionResult<AdministrationDto>> GetAsync()
        {
            var administration = await _administrationService.GetAsync();
            return Ok(administration);
        }

        [HttpPut]
        [RequiresPermission(Permissions.AdministrationUpdate)]
        public async Task<ActionResult> UpdateAsync([FromBody] AdministrationDto administrationDto)
        {
            await _administrationService.UpdateAsync(administrationDto);
            return Ok();
        }

    }
}
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
    public class AdministrationController(IAdministrationService administrationService)
        : ControllerBase
    {
        private readonly IAdministrationService _administrationService = administrationService;

        [HttpGet]
        public async Task<ActionResult<AdministrationDto>> GetAsync()
        {
            var administration = await _administrationService.GetAsync();
            return Ok(administration);
        }

        [HttpPut]
        [RequiresPermission(Permissions.All, Permissions.AdministrationUpdate)]
        public async Task<ActionResult> UpdateAsync([FromBody] AdministrationDto administrationDto)
        {
            if (!Validate(administrationDto))
            {
                return BadRequest();
            }

            await _administrationService.UpdateAsync(administrationDto);
            return Ok();
        }

        private static bool Validate(AdministrationDto administrationDto)
        {
            return administrationDto != null
                && administrationDto.HeadOfDepartment is not null
                && administrationDto.ScientificSecretary is not null
                && administrationDto.HeadOfDepartment.FirstName is not null
                && administrationDto.HeadOfDepartment.LastName is not null
                && administrationDto.HeadOfDepartment.Patronymic is not null
                && administrationDto.ScientificSecretary.FirstName is not null
                && administrationDto.ScientificSecretary.LastName is not null
                && administrationDto.ScientificSecretary.Patronymic is not null;
        }
    }
}

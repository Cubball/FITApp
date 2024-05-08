using FITApp.Auth.Attributes;
using FITApp.Auth.Data;
using FITApp.EmployeesService.Dtos;
using FITApp.EmployeesService.Interfaces;
using FITApp.EmployeesService.Models;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FITApp.EmployeesService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeesService _employeeService;

        public EmployeesController(IEmployeesService employeeService)
        {
            _employeeService = employeeService;
        }
        // public EmployeesController(IMapper mapper, IEmployeesService employeeService, IUsersService usersService)
        // {
        //     _mapper = mapper;
        //     _employeeService = employeeService;
        //     _usersService = usersService;
        // }


        [RequiresPermission(Permissions.UsersCreate, Permissions.All)]
        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] EmployeeDto employeeDto)
        {
            await _employeeService.CreateEmployee(employeeDto);
            return Ok();
        }


        //TODO: set bether name for method
        [RequiresPermission(Permissions.UsersUpdate, Permissions.All)]
        [HttpPut("{id}")]
        public async Task<IActionResult> SetFullNameAndBirth(string id, [FromBody] EmployeeDetailsDto employeeDetails)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Invalid employee ID.");
            }

            long updatedCount = await _employeeService.UpdateEmployeeDetails(id, employeeDetails);
            return updatedCount == 0 ? NotFound() : Ok();

        }

        [RequiresPermission(Permissions.UsersDelete, Permissions.All)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            long deletedCount = await _employeeService.DeleteEmployee(id);

            if (deletedCount == 0)
            {
                return NotFound(); // Якщо елемент не знайдено
            }

            return NoContent(); // Успішний видалення, не потрібно повертати тіло відповіді
        }


        [RequiresPermission(Permissions.UsersRead, Permissions.All)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee(string id)
        {
            var employee = await _employeeService.GetEmployee(id);
            return Ok(employee);
        }

        [RequiresPermission(Permissions.UsersUpdate, Permissions.All)]
        [HttpPost("{id}/positions")]
        public async Task<IActionResult> AddPosition(string id, [FromBody] PositionDto positionDto)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Invalid employee ID.");
            }
            try
            {
                long updatedCount = await _employeeService.UpdateEmployeePositions(id, positionDto);
                return updatedCount == 0 ? NotFound() : Ok();

            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Errors);
                throw;
            }

        }

        [RequiresPermission(Permissions.UsersUpdate, Permissions.All)]
        [HttpPost("{id}/educations")]
        public async Task<IActionResult> AddEducation(string id, [FromBody] EducationDto educationDto)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Invalid employee ID.");
            }
            try
            {
                long updatedCount = await _employeeService.UpdateEmployeeEducations(id, educationDto);
                return updatedCount == 0 ? NotFound() : Ok();
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Errors);
                throw;
            }


        }

        [RequiresPermission(Permissions.UsersUpdate, Permissions.All)]
        [HttpPost("{id}/academic-degrees")]
        public async Task<IActionResult> AddAcademicDegree(string id, [FromBody] AcademicDegreeDto educationDto)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Invalid employee ID.");
            }
            try
            {
                long updatedCount = await _employeeService.UpdateEmployeeAcademicDegrees(id, educationDto);
                return updatedCount == 0 ? NotFound() : Ok();
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Errors);
                throw;
            }


        }

        [RequiresPermission(Permissions.UsersUpdate, Permissions.All)]
        [HttpPost("{id}/academic-ranks")]
        public async Task<IActionResult> AddAcademicRank(string id, [FromBody] AcademicRankDto academicRankDto)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Invalid employee ID.");
            }
            try
            {
                long updatedCount = await _employeeService.UpdateEmployeeAcademicRanks(id, academicRankDto);
                return updatedCount == 0 ? NotFound() : Ok();
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Errors);
                throw;
            }

        }

        [RequiresPermission(Permissions.UsersUpdate, Permissions.All)]
        [HttpDelete("{id}/academic-ranks/{index}")]
        public async Task<IActionResult> RemoveEmployeeAcademicRank(string id, int index)
        {
            var result = await _employeeService.RemoveEmployeeAcademicRankByIndex(id, index);
            return result == 0 ? NotFound() : Ok();
        }


        [RequiresPermission(Permissions.UsersUpdate, Permissions.All)]
        [HttpDelete("{id}/positions/{index}")]
        public async Task<IActionResult> RemoveEmployeePosition(string id, int index)
        {
            var result = await _employeeService.RemoveEmployeePositionByIndex(id, index);
            return result == 0 ? NotFound() : Ok();
        }

        [RequiresPermission(Permissions.UsersUpdate, Permissions.All)]
        [HttpDelete("{id}/educations/{index}")]
        public async Task<IActionResult> RemoveEmployeeEducation(string id, int index)
        {
            var result = await _employeeService.RemoveEmployeeEducationByIndex(id, index);
            return result == 0 ? NotFound() : Ok();
        }

        [RequiresPermission(Permissions.UsersUpdate, Permissions.All)]
        [HttpDelete("{id}/academic-degrees/{index}")]
        public async Task<IActionResult> RemoveEmployeeAcademicDegree(string id, int index)
        {
            var result = await _employeeService.RemoveEmployeeAcademicDegreeByIndex(id, index);
            return result == 0 ? NotFound() : Ok();
        }

        [RequiresPermission(Permissions.UsersRead, Permissions.All)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployeesAsync(uint page = 1, uint pageSize = 10)
        {
            var response = await _employeeService.GetEmployeesPagination(page, pageSize);
            return Ok(response);
        }
        // [HttpGet]
        // public async Task<IActionResult> GetEmployees()
        // {
        //     var employees = await _employeeService.GetEmployees();
        //     return Ok(employees);
        // }
    }
}
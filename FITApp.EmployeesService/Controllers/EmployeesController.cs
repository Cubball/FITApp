using AutoMapper;
using FITApp.EmployeesService.Dtos;
using FITApp.EmployeesService.Interfaces;
using FITApp.EmployeesService.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace FITApp.EmployeesService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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


        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] EmployeeDto employeeDto)
        {
            await _employeeService.CreateEmployee(employeeDto);
            return Ok();
        }


        //TODO: set bether name for method
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


        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee(string id)
        {
            var employee = await _employeeService.GetEmployee(id);
            return Ok(employee);
        }
        // [HttpGet]
        // public async Task<IActionResult> GetEmployee()
        // {
        //     var id = HttpContext.User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
        //     var employee = await _employeeService.GetEmployee(id);
        //     return Ok(employee);
        // }

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
        [HttpDelete("{id}/academic-ranks/{index}")]
        public async Task<IActionResult> RemoveEmployeeAcademicRank(string id, int index)
        {
            var result = await _employeeService.RemoveEmployeeAcademicRankByIndex(id, index);
            return result == 0 ? NotFound() : Ok();
        }
        [HttpDelete("{id}/positions/{index}")]
        public async Task<IActionResult> RemoveEmployeePosition(string id, int index)
        {
            var result = await _employeeService.RemoveEmployeePositionByIndex(id, index);
            return result == 0 ? NotFound() : Ok();
        }

        [HttpDelete("{id}/educations/{index}")]
        public async Task<IActionResult> RemoveEmployeeEducation(string id, int index)
        {
            var result = await _employeeService.RemoveEmployeeEducationByIndex(id, index);
            return result == 0 ? NotFound() : Ok();
        }

        [HttpDelete("{id}/academic-degrees/{index}")]
        public async Task<IActionResult> RemoveEmployeeAcademicDegree(string id, int index)
        {
            var result = await _employeeService.RemoveEmployeeAcademicDegreeByIndex(id, index);
            return result == 0 ? NotFound() : Ok();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployeesAsync(int page = 1, int pageSize = 10)
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

        [HttpPut("{id}/photo")]
        public async Task<IActionResult> AddPhoto(string id, [FromForm] EmployeePhotoUploadDto employeePhotoUploadDto)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Invalid employee ID.");
            }
            try
            {
                long updatedCount = await _employeeService.UpdateEmployeePhoto(id, employeePhotoUploadDto);
                return updatedCount == 0 ? NotFound() : Ok();
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Errors);
                throw;
            }
        }
    }
}
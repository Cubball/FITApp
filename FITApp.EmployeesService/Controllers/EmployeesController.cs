using AutoMapper;
using FITApp.EmployeesService.Dtos;
using FITApp.EmployeesService.Interfaces;
using FITApp.EmployeesService.Models;
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


        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            var employees = await _employeeService.GetEmployees();
            return Ok(employees);
        }

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

            long updatedCount = await _employeeService.UpdateEmployeePositions(id, positionDto);

            return updatedCount == 0 ? NotFound() : Ok();
        }




    }
}


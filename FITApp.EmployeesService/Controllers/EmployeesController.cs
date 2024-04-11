using AutoMapper;
using FITApp.EmployeesService.Dtos;
using FITApp.EmployeesService.Interfaces;
using FITApp.EmployeesService.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Microsoft.IdentityModel.JsonWebTokens;

namespace FITApp.EmployeesService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IEmployeesService _employeeService;
        public EmployeesController(IMapper mapper, IEmployeesService employeeService)
        {
            _mapper = mapper;
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            var employees = await _employeeService.GetEmployees();
            return Ok(employees);
        }
        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] EmployeeDto employeeDto)
        {
            Employee employee = _mapper.Map<Employee>(employeeDto);
            await _employeeService.CreateEmployee(employee);
            return Ok(employee);
        }

        //TODO: set bether name for method
        [HttpPut("{id}")]
        public async Task<IActionResult> SetFullNameAndBirth(string id, [FromBody] EmployeeDetailsDto employeeDetails)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Invalid employee ID.");
            }
            try
            {

                UpdateResult updateResult = await _employeeService.UpdateEmployeeDetails(id, employeeDetails);
                return updateResult.ModifiedCount == 0 ? NotFound() : Ok();
            }
            catch (Exception ex)
            {
                // Обробка помилок
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                DeleteResult deleteResult = await _employeeService.DeleteEmployee(id);

                if (deleteResult.DeletedCount == 0)
                {
                    return NotFound(); // Якщо елемент не знайдено
                }

                return NoContent(); // Успішний видалення, не потрібно повертати тіло відповіді
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Помилка сервера: {e.Message}"); // Помилка сервера
            }
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






    }
}
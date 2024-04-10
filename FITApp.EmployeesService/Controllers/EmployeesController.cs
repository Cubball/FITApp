using AutoMapper;
using FITApp.EmployeesService.Dtos;
using FITApp.EmployeesService.Interfaces;
using FITApp.EmployeesService.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace FITApp.EmployeesService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeesRepository _employeeRepository;
        private readonly IMapper _mapper;
        private readonly IEmployeesService _employeeService;
        public EmployeesController(IEmployeesRepository employeeRepository, IMapper mapper, IEmployeesService employeeService)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            var employees = await _employeeRepository.GetEmployees();
            return Ok(employees);
        }
        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] EmployeeDto employeeDto)
        {
            var employee = _mapper.Map<Employee>(employeeDto);
            await _employeeRepository.CreateEmployee(employee, _employeeRepository.GetResult());
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

                // DateOnly newDateFromDateTime = new(employeeUpdateDto.BirthDate.Year,
                //                                    employeeUpdateDto.BirthDate.Month,
                //                                    employeeUpdateDto.BirthDate.Day);
                // UpdateDefinition<Employee> update = Builders<Employee>.Update
                //     .Set(employee => employee.FirstName, employeeUpdateDto.FirstName)
                //     .Set(employee => employee.LastName, employeeUpdateDto.LastName)
                //     .Set(employee => employee.Patronymic, employeeUpdateDto.Patronymic)
                //     .Set(employee => employee.BirthDate, newDateFromDateTime);

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
                DeleteResult deleteResult = await _employeeRepository.DeleteEmployee(id);

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



    }
}
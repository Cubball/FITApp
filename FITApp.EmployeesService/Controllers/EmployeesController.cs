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
        public EmployeesController(IEmployeesRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
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
            await _employeeRepository.CreateEmployee(employee);
            return Ok(employee);
        }

        //TODO: set bether name fir method
        [HttpPut("{id}")]
        public async Task<IActionResult> SetFullNameAndBirth(string id, [FromBody] EmployeeUpdateDto employeeUpdateDto)
        {
            if (id != null)
            {
                var employee = await _employeeRepository.GetEmployee(id);
                if (employee == null)
                {
                    return NotFound();
                }
                DateOnly newDateFromDateTime = new DateOnly(employeeUpdateDto.BirthDate.Year,
                                                        employeeUpdateDto.BirthDate.Month,
                                                        employeeUpdateDto.BirthDate.Day);

                UpdateDefinition<Employee> update = Builders<Employee>.Update
                    .Set(employee => employee.FirstName, employeeUpdateDto.FirstName)
                    .Set(employee => employee.LastName, employeeUpdateDto.LastName)
                    .Set(employee => employee.Patronymic, employeeUpdateDto.Patronymic)
                    .Set(employee => employee.BirthDate, newDateFromDateTime);
                // _mapper.Map(employeeUpdateDto, employee);    

                await _employeeRepository.UpdateEmployee(id, update);
                return Ok();
            }
            return BadRequest();
        }



    }
}
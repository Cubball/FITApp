using FITApp.EmployeesService.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FITApp.EmployeesService.Controllers
{
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeesRepository _employeeRepository;

        public EmployeesController(IEmployeesRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            var employees = await _employeeRepository.GetEmployees();
            return Ok(employees);
        }

        //public async Task<IActionResult<Employee>> 


    }
}
using System.IdentityModel.Tokens.Jwt;
using FITApp.EmployeesService.Dtos;
using FITApp.EmployeesService.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FITApp.EmployeesService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProfileController : ControllerBase
{
    private readonly IEmployeesService _employeeService;

    public ProfileController(IEmployeesService employeeService)
    {
        _employeeService = employeeService;
    }
    [HttpGet]
    public async Task<IActionResult> GetEmployee()
    {
        var id = HttpContext.User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
        if (string.IsNullOrEmpty(id))
        {
            return BadRequest("Invalid employee ID.");
        }
        var employee = await _employeeService.GetEmployee(id);
        return Ok(employee);
    }
    [HttpPut]
    public async Task<IActionResult> SetFullNameAndBirth([FromBody] EmployeeDetailsDto employeeDetails)
    {
        var id = HttpContext.User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
        if (string.IsNullOrEmpty(id))
        {
            return BadRequest("Invalid employee ID.");
        }

        long updatedCount = await _employeeService.UpdateEmployeeDetails(id, employeeDetails);
        return updatedCount == 0 ? NotFound() : Ok();

    }





}
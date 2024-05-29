using System.IdentityModel.Tokens.Jwt;
using FITApp.EmployeesService.Dtos;
using FITApp.EmployeesService.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FITApp.EmployeesService.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ProfileController : ControllerBase
{
    private readonly IEmployeesService _employeeService;
    private readonly IPhotoService _photoService;

    public ProfileController(IEmployeesService employeeService, IPhotoService photoService)
    {
        _employeeService = employeeService;
        _photoService = photoService;
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

        try
        {
            long updatedCount = await _employeeService.UpdateEmployeeDetails(id, employeeDetails);
            return updatedCount == 0 ? NotFound() : Ok();

        }
        catch (ValidationException ex)
        {

            return BadRequest(ex.Errors);
            throw;
        }

    }

    [HttpPost("positions")]
    public async Task<IActionResult> AddPosition([FromBody] PositionDto positionDto)
    {

        var id = HttpContext.User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
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
    [HttpPost("educations")]
    public async Task<IActionResult> AddEducation([FromBody] EducationDto educationDto)
    {
        var id = HttpContext.User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

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

    [HttpPost("academic-degrees")]
    public async Task<IActionResult> AddAcademicDegree([FromBody] AcademicDegreeDto educationDto)
    {
        var id = HttpContext.User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
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

    [HttpPost("academic-ranks")]
    public async Task<IActionResult> AddAcademicRank([FromBody] AcademicRankDto academicRankDto)
    {
        var id = HttpContext.User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

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
    [HttpDelete("academic-ranks/{index}")]
    public async Task<IActionResult> RemoveEmployeeAcademicRank(int index)
    {
        var id = HttpContext.User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
        if (string.IsNullOrEmpty(id))
        {
            return BadRequest("Invalid employee ID.");
        }

        var result = await _employeeService.RemoveEmployeeAcademicRankByIndex(id, index);
        return result == 0 ? NotFound() : Ok();
    }
    [HttpDelete("positions/{index}")]
    public async Task<IActionResult> RemoveEmployeePosition(int index)
    {
        var id = HttpContext.User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
        if (string.IsNullOrEmpty(id))
        {
            return BadRequest("Invalid employee ID.");
        }

        var result = await _employeeService.RemoveEmployeePositionByIndex(id, index);
        return result == 0 ? NotFound() : Ok();
    }

    [HttpDelete("educations/{index}")]
    public async Task<IActionResult> RemoveEmployeeEducation(int index)
    {
        var id = HttpContext.User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
        if (string.IsNullOrEmpty(id))
        {
            return BadRequest("Invalid employee ID.");
        }

        var result = await _employeeService.RemoveEmployeeEducationByIndex(id, index);
        return result == 0 ? NotFound() : Ok();
    }

    [HttpDelete("academic-degrees/{index}")]
    public async Task<IActionResult> RemoveEmployeeAcademicDegree(int index)
    {
        var id = HttpContext.User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
        if (string.IsNullOrEmpty(id))
        {
            return BadRequest("Invalid employee ID.");
        }

        var result = await _employeeService.RemoveEmployeeAcademicDegreeByIndex(id, index);
        return result == 0 ? NotFound() : Ok();
    }

    [HttpPut("photo")]
    public async Task<IActionResult> AddPhoto([FromForm] EmployeePhotoUploadDto employeePhotoUploadDto)
    {
        var id = HttpContext.User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
        if (string.IsNullOrEmpty(id))
        {
            return BadRequest("Invalid employee ID.");
        }
        try
        {
            var employee = await _employeeService.GetEmployee(id);
            if (employee.Photo != "") { long updatedCountPhoto = await _photoService.RemoveEmployeePhoto(id); }
            long updatedCount = await _photoService.UpdateEmployeePhoto(id, employeePhotoUploadDto);
            return updatedCount == 0 ? NotFound() : Ok();
        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Errors);
            throw;
        }
    }
    [HttpDelete("photo")]
    public async Task<IActionResult> RemovePhoto()
    {
        var id = HttpContext.User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
        if (string.IsNullOrEmpty(id))
        {
            return BadRequest("Invalid employee ID.");
        }

        try
        {
            long updatedCount = await _photoService.RemoveEmployeePhoto(id);
            return updatedCount == 0 ? NotFound() : Ok();
        }
        catch (Exception)
        {
            return StatusCode(500, "An error occurred while removing the employee's photo.");
        }
    }
}
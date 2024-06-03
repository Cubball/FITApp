using AutoMapper;
using FITApp.EmployeesService.Dtos;
using FITApp.EmployeesService.Models;

namespace FITApp.EmployeesService;

public class AuthorProfile: Profile
{
    public AuthorProfile()
    {
        CreateMap<AuthorDto, EmployeeDetailsDto>().ReverseMap();
    }
}
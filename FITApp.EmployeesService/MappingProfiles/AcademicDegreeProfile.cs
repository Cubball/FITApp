using AutoMapper;
using FITApp.EmployeesService.Dtos;
using FITApp.EmployeesService.Models;

namespace FITApp.EmployeesService;

public class AcademicDegreeProfile: Profile
{
    public AcademicDegreeProfile()
    {
        CreateMap<AcademicDegree, AcademicDegreeDto>().ReverseMap();
    }
}
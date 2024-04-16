using AutoMapper;
using FITApp.EmployeesService.Dtos;
using FITApp.EmployeesService.Models;

namespace FITApp.EmployeesService;

public class AcademicRankProfile: Profile
{
    public AcademicRankProfile()
    {
        CreateMap<AcademicRank, AcademicRankDto>().ReverseMap();
    }
}
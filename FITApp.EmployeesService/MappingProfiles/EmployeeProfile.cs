using AutoMapper;
using FITApp.EmployeesService.Dtos;
using FITApp.EmployeesService.Models;

namespace FITApp.EmployeesService
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeDto>()
                .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate));

            CreateMap<EmployeeDto, Employee>()
                .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate));

            CreateMap<DateTime, DateOnly>().ConvertUsing(new DateTimeToDateOnlyConverter());
            CreateMap<DateOnly, DateTime>().ConvertUsing(date => new DateTime(date.Year, date.Month, date.Day));

            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Position, PositionDto>().ReverseMap();
            CreateMap<Education, EducationDto>().ReverseMap();
            CreateMap<AcademicDegree, AcademicDegreeDto>().ReverseMap();
            CreateMap<AcademicRank, AcademicRankDto>().ReverseMap();
        }
    }
}
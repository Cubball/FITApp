using AutoMapper;
using FITApp.EmployeesService.Models;
using FITApp.EmployeesService.Dtos;

namespace FITApp.EmployeesService;

public class EducationProfile: Profile
{
        public EducationProfile()
        {
            CreateMap<Education, EducationDto>().ReverseMap();
            CreateMap<DateTime, DateOnly>().ConvertUsing(new DateTimeToDateOnlyConverter());
            CreateMap<DateOnly, DateTime>().ConvertUsing(date => new DateTime(date.Year, date.Month, date.Day));
        }
}
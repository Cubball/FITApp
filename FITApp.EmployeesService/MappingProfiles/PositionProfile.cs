using AutoMapper;
using FITApp.EmployeesService.Dtos;
using FITApp.EmployeesService.Models;

namespace FITApp.EmployeesService.MappingProfiles
{
    public class PositionProfile : Profile
    {
        public PositionProfile()
        {

            CreateMap<Position, PositionDto>().ReverseMap();
            CreateMap<DateTime, DateOnly>().ConvertUsing(new DateTimeToDateOnlyConverter());
            CreateMap<DateOnly, DateTime>().ConvertUsing(date => new DateTime(date.Year, date.Month, date.Day));

        }
    }

}
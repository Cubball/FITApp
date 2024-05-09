using AutoMapper;
using FITApp.EmployeesService.Dtos;
using FITApp.EmployeesService.Models;

namespace FITApp.EmployeesService.MappingProfiles
{
    public class PhotoProfile:Profile
    {
        public PhotoProfile()
        {
            CreateMap<EmployeePhotoUpload, EmployeePhotoUploadDto>().ReverseMap();
        }
    }
}

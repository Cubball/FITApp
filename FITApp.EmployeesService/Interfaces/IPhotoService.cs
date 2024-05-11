using FITApp.EmployeesService.Dtos;

namespace FITApp.EmployeesService.Interfaces
{
    public interface IPhotoService
    {
        Task<long> UpdateEmployeePhoto(string id, EmployeePhotoUploadDto photoUploadDto);
        Task<long> RemoveEmployeePhoto(string id);

    }
}

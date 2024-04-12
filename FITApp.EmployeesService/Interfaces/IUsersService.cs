using FITApp.EmployeesService.Dtos;
using FITApp.EmployeesService.Models;

namespace FITApp.EmployeesService.Interfaces
{
    public interface IUserService
    {
        Task<long> CreateUser(UserDto user);
        Task<long> DeleteUser(string id);
        Task<long> UpdateUserDetails(string id, UserUpdateDto userUpdateDto);
    }
}
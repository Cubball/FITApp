using FITApp.EmployeesService.Dtos;

namespace FITApp.EmployeesService.Interfaces
{
    public interface IUsersService
    {
        Task CreateUser(UserDto user);
        Task<long> DeleteUser(string id);
        Task<long> UpdateUserDetails(string id, UserUpdateDto userUpdateDto);
    }
}
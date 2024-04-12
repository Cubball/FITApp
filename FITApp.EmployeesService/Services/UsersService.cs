using FITApp.EmployeesService.Dtos;
using FITApp.EmployeesService.Interfaces;
using FITApp.EmployeesService.Models;

namespace FITApp.EmployeesService.Services
{
    public class UsersService : IUsersService
    {
        private readonly IEmployeesRepository _employeesRepository;

        public UsersService(IEmployeesRepository employeesRepository)
        {
            _employeesRepository = employeesRepository;
        }
        public async Task CreateUser(UserDto user)
        {
            var employee = new Employee
            {
                Id = user.UserId,
                User = new User
                {
                    UserId = user.UserId,
                    Email = user.Email,
                    Role = user.Role,
                    RoleId = user.RoleId
                }
            };
            await _employeesRepository.CreateEmployee(employee);
        }

        public Task<long> DeleteUser(string id)
        {
            throw new NotImplementedException();
        }

        public Task<long> UpdateUserDetails(string id, UserUpdateDto userUpdateDto)
        {
            throw new NotImplementedException();
        }
    }
}
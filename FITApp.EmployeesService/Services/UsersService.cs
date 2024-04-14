using FITApp.EmployeesService.Dtos;
using FITApp.EmployeesService.Interfaces;
using FITApp.EmployeesService.Models;
using MongoDB.Driver;

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

        public async Task<long> DeleteUser(string id)
        {
            DeleteResult result = await _employeesRepository.DeleteEmployee(id);
            return result.DeletedCount;
        }

        public async Task<long> UpdateUserDetails(string id, UserUpdateDto userUpdateDto)
        {
            UpdateDefinition<Employee> update = Builders<Employee>.Update
                .Set(employee => employee.User.Email, userUpdateDto.Email)
                .Set(employee => employee.User.Role, userUpdateDto.Role)
                .Set(employee => employee.User.RoleId, userUpdateDto.RoleId);
            UpdateResult result = await _employeesRepository.UpdateEmployee(id, update);
            return result.ModifiedCount;
        }
    }
}
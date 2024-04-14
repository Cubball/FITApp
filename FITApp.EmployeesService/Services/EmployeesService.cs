using FITApp.EmployeesService.Dtos;
using FITApp.EmployeesService.Interfaces;
using FITApp.EmployeesService.Models;
using MongoDB.Driver;

namespace FITApp.EmployeesService.Services
{

    public class EmployeesService : IEmployeesService
    {
        private readonly IEmployeesRepository _employeesRepository;
        public EmployeesService(IEmployeesRepository employeeRepository)
        {
            _employeesRepository = employeeRepository;
        }

        public async Task CreateEmployee(Employee employee)
        {
            await _employeesRepository.CreateEmployee(employee);
        }

        public async Task<long> DeleteEmployee(string id)
        {
            var result = await _employeesRepository.DeleteEmployee(id);
            return result.DeletedCount;
        }

        public async Task<Employee> GetEmployee(string id)
        {
            return await _employeesRepository.GetEmployee(id);
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await _employeesRepository.GetEmployees();
        }

        public async Task<long> UpdateEmployeeDetails(string id, EmployeeDetailsDto employeeDetails)
        {
            DateOnly newDateFromDateTime = new(employeeDetails.BirthDate.Year,
                                   employeeDetails.BirthDate.Month,
                                   employeeDetails.BirthDate.Day);
            UpdateDefinition<Employee> update = Builders<Employee>.Update
                .Set(employee => employee.FirstName, employeeDetails.FirstName)
                .Set(employee => employee.LastName, employeeDetails.LastName)
                .Set(employee => employee.Patronymic, employeeDetails.Patronymic)
                .Set(employee => employee.BirthDate, newDateFromDateTime);
            var result = await _employeesRepository.UpdateEmployee(id, update);
            return result.ModifiedCount;
        }
    }
}
using FITApp.EmployeesService.Dtos;
using FITApp.EmployeesService.Interfaces;
using FITApp.EmployeesService.Models;
using MongoDB.Driver;

namespace FITApp.EmployeesService.Services
{

    public class EmployeesService : IEmployeesService
    {
        private readonly IEmployeesRepository _employeeRepository;
        public EmployeesService(IEmployeesRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<Employee> CreateEmployee(Employee employee)
        {
            await _employeeRepository.CreateEmployee(employee, _employeeRepository.GetResult());
            return employee;
        }

        public Task<DeleteResult> DeleteEmployee(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Employee> GetEmployee(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Employee>> GetEmployees()
        {
            throw new NotImplementedException();
        }

        public async Task<UpdateResult> UpdateEmployeeDetails(string id, EmployeeDetailsDto employeeDetails)
        {
            DateOnly newDateFromDateTime = new(employeeDetails.BirthDate.Year,
                                   employeeDetails.BirthDate.Month,
                                   employeeDetails.BirthDate.Day);
            UpdateDefinition<Employee> update = Builders<Employee>.Update
                .Set(employee => employee.FirstName, employeeDetails.FirstName)
                .Set(employee => employee.LastName, employeeDetails.LastName)
                .Set(employee => employee.Patronymic, employeeDetails.Patronymic)
                .Set(employee => employee.BirthDate, newDateFromDateTime);

            return await _employeeRepository.UpdateEmployee(id, update);
        }
    }
}
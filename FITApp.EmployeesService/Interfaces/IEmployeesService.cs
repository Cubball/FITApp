using FITApp.EmployeesService.Dtos;
using FITApp.EmployeesService.Models;
using MongoDB.Driver;

namespace FITApp.EmployeesService.Interfaces
{
    public interface IEmployeesService
    {
        Task<IEnumerable<Employee>> GetEmployees();
        Task<Employee> GetEmployee(string id);
        Task<Employee> CreateEmployee(Employee employee);
        Task<long> DeleteEmployee(string id);
        Task<long> UpdateEmployeeDetails(string id, EmployeeDetailsDto employeeDetails);

    }
}
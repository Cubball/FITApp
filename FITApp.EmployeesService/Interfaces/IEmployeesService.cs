using FITApp.EmployeesService.Dtos;
using FITApp.EmployeesService.Models;
using MongoDB.Driver;

namespace FITApp.EmployeesService.Interfaces
{
    public interface IEmployeesService
    {
        Task<UpdateResult> UpdateEmployeeDetails(string id, EmployeeDetailsDto employeeDetails);
        Task<IEnumerable<Employee>> GetEmployees();
        Task<Employee> GetEmployee(string id);
        Task<Employee> CreateEmployee(Employee employee);
        Task<DeleteResult> DeleteEmployee(string id);

    }
}
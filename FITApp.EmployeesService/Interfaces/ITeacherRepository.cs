using FITApp.EmployeesService.Models;
using MongoDB.Driver;

namespace FITApp.EmployeesService.Interfaces
{
    public interface IEmployeesRepository
    {
        Task<IEnumerable<Employee>> GetEmployees();
        Task<Employee> GetEmployee(string id);
        Task<Employee> CreateEmployee(Employee employee);
        Task<UpdateResult> UpdateEmployee(string id, UpdateDefinition<Employee> update);
        Task<DeleteResult> DeleteEmployee(string id);
    }
}
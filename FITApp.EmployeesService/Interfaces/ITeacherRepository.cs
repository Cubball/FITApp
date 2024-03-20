using FITApp.EmployeesService.Models;

namespace FITApp.EmployeesService.Interfaces
{
    public interface IEmployeesRepository
    {
        Task<IEnumerable<Employee>> GetEmployees();
        Task<Employee> GetEmployee(string id);
        Task<Employee> CreateEmployee(Employee teacher);
        //Task UpdateEmployee(string id, Employee teacher);
        Task DeleteEmployee(string id);
    }
}
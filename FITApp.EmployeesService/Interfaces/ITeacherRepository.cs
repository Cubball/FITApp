using FITApp.EmployeesService.Models;

namespace FITApp.EmployeesService.Interfaces
{
    public interface IEmployeesRepository
    {
        Task<IEnumerable<Employee>> GetTeachers();
        Task<Employee> GetTeacher(string id);
        Task<Employee> CreateTeacher(Employee teacher);
        Task UpdateTeacher(string id, Employee teacher);
        Task DeleteTeacher(string id);
    }
}
using System.Linq.Expressions;
using FITApp.EmployeesService.Models;
using MongoDB.Driver;

namespace FITApp.EmployeesService.Interfaces
{
    public interface IEmployeesRepository
    {
        Task<IEnumerable<Employee>> GetEmployees();
        Task<Employee> GetEmployee(string id);
        Task CreateEmployee(Employee employee);
        Task<UpdateResult> UpdateEmployee(string id, UpdateDefinition<Employee> update);
        Task<DeleteResult> DeleteEmployee(string id);
        Task<List<AcademicRank>> GetAcademicRanksByEmployeeId(string id);
        Task<UpdateResult> RemoveArrayElementByIndex<TElement>(
                                                                string id,
                                                                int index,
                                                                Func<Employee, List<TElement>> selector,
                                                                Expression<Func<Employee, object>> expression);

    }
}
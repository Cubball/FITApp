using System.Linq.Expressions;
using FITApp.EmployeesService.Models;
using MongoDB.Bson;
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
        Task<UpdateResult> RemoveArrayElementByIndex<TElement>(string id,
                                                                int index,
                                                                Func<Employee, List<TElement>> selector,
                                                                Expression<Func<Employee, object>> expression);
        Task<long> TotalCountDocuments(FilterDefinition<Employee> filter);
        
        // Task<IEnumerable<Employee>> GetEmployeesByPage(FilterDefinition<Employee> filter, int page, int pageSize);
        // Task<IEnumerable<SimpleEmployeeDto>> GetEmployeesByPage2(FilterDefinition<Employee> filter, int page, int pageSize);

        // Task<List<BsonDocument>> GetEmployeesByPage3(FilterDefinition<Employee> filter,
        //     IProjection<Employee> projection, int page, int pageSize);

        Task<IEnumerable<BsonDocument>> GetEmployeesByPage(FilterDefinition<Employee> filter,
            ProjectionDefinition<Employee> projection, int page, int pageSize);
    }
}
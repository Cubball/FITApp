using FITApp.EmployeesService.Interfaces;
using FITApp.EmployeesService.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace FITApp.EmployeesService.Repositories
{
    public class EmployeesRepository : IEmployeesRepository
    {
        private readonly IMongoCollection<Employee> _employeesCollection;

        public EmployeesRepository(IOptions<MongoDbSettings> mongoDbSettings)
        {
            MongoClient client = new MongoClient(mongoDbSettings.Value.ConnectionString);
            IMongoDatabase database = client.GetDatabase(mongoDbSettings.Value.DatabaseName);
            _employeesCollection = database.GetCollection<Employee>("employee");
        }
        public Task<Employee> CreateTeacher(Employee teacher)
        {
            throw new NotImplementedException();
        }

        public Task DeleteTeacher(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Employee> GetTeacher(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Employee>> GetTeachers()
        {
            throw new NotImplementedException();
        }

        public Task UpdateTeacher(string id, Employee teacher)
        {
            throw new NotImplementedException();
        }
    }

}
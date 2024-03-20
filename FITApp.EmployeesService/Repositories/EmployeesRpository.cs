using FITApp.EmployeesService.Interfaces;
using FITApp.EmployeesService.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
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

        public async Task<Employee> CreateEmployee(Employee teacher)
        {
            await _employeesCollection.InsertOneAsync(teacher);
            return teacher;
        }

        public async Task DeleteEmployee(string id)
        {
            FilterDefinition<Employee> filter = Builders<Employee>.Filter.Eq("Id", id);
            await _employeesCollection.DeleteOneAsync(filter);
            return;

        }

        public async Task<Employee> GetEmployee(string id)
        {
            FilterDefinition<Employee> filter = Builders<Employee>.Filter.Eq("Id", id);
            return await _employeesCollection.Find(filter).FirstOrDefaultAsync();
        }


        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await _employeesCollection.Find(new BsonDocument()).ToListAsync();
        }
    }

}
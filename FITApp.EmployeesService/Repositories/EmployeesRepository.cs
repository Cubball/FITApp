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
            MongoClient client = new(mongoDbSettings.Value.ConnectionString);
            IMongoDatabase database = client.GetDatabase(mongoDbSettings.Value.DatabaseName);
            _employeesCollection = database.GetCollection<Employee>("employees");
        }

        public async Task CreateEmployee(Employee employee)
        {
            await _employeesCollection.InsertOneAsync(employee);
        }

        public async Task<DeleteResult> DeleteEmployee(string id)
        {
            FilterDefinition<Employee> filterDefinition = Builders<Employee>.Filter.Eq(e => e.Id, id);
            return await _employeesCollection.DeleteOneAsync(filterDefinition);
        }

        public async Task<Employee> GetEmployee(string id)
        {
            FilterDefinition<Employee> filter = Builders<Employee>.Filter.Eq(a => a.Id, id);
            return await _employeesCollection.Find(filter).FirstOrDefaultAsync();
        }


        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await _employeesCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<UpdateResult> UpdateEmployee(string id, UpdateDefinition<Employee> update)
        {
            FilterDefinition<Employee> filter = Builders<Employee>.Filter.
                Eq(employee => employee.Id, id);

            return await _employeesCollection.UpdateOneAsync(filter, update);
        }

        // public async Task UpdateEmployee(Employee employee)
        // {
        //     var filter = Builders<Employee>.Filter.Eq("Id", employee.Id);
        //     await _employeesCollection.ReplaceOneAsync(filter, employee);
        // }
    }

}
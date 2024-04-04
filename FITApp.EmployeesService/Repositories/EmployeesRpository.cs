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

        public async Task<Employee> CreateEmployee(Employee employee)
        {
            await _employeesCollection.InsertOneAsync(employee);
            return employee;
        }

        public async Task DeleteEmployee(string id)
        {
            FilterDefinition<Employee> filter = Builders<Employee>.Filter.Eq("Id", id);
            await _employeesCollection.DeleteOneAsync(filter);
            return;

        }

        public async Task<Employee> GetEmployee(string id)
        {
            var filter = Builders<Employee>.Filter.Eq(a => a.Id, ObjectId.Parse(id));
            return await _employeesCollection.Find(filter).FirstOrDefaultAsync();
        }


        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await _employeesCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<UpdateResult> UpdateEmployee(string id, UpdateDefinition<Employee> update)
        {
            FilterDefinition<Employee> filter = Builders<Employee>.Filter.
                Eq(employee => employee.Id, ObjectId.Parse(id));

            return await _employeesCollection.UpdateOneAsync(filter, update);
        }

        // public async Task UpdateEmployee(Employee employee)
        // {
        //     var filter = Builders<Employee>.Filter.Eq("Id", employee.Id);
        //     await _employeesCollection.ReplaceOneAsync(filter, employee);
        // }
    }

}
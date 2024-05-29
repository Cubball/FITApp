using System.Linq.Expressions;
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

        public async Task<List<AcademicRank>> GetAcademicRanksByEmployeeId(string id)
        {
            var employee = await GetEmployee(id);

            return employee.AcademicRanks;
        }
        public async Task<UpdateResult> RemoveArrayElementByIndex<TElement>(
                                                                string id,
                                                                int index,
                                                                Func<Employee, List<TElement>> selector,
                                                                Expression<Func<Employee, object>> expression)
        {
            if (index < 0)
            {
                return new UpdateResult.Acknowledged(0, 0, null);
            }

            var employee = await GetEmployee(id);
            var elements = selector(employee);
            if (index >= elements.Count)
            {
                return new UpdateResult.Acknowledged(0, 0, null);
            }

            elements.RemoveAt(index);
            var update = elements.Count > 0
                ? Builders<Employee>.Update.Set(expression, elements)
                : Builders<Employee>.Update.Unset(expression);
            var result = await UpdateEmployee(id, update);
            
            return result;
        }

        public async Task<long> TotalCountDocuments(FilterDefinition<Employee> filter) => 
            await _employeesCollection.CountDocumentsAsync(filter);

 
        public async Task<IEnumerable<BsonDocument>> GetEmployeesByPage(
            FilterDefinition<Employee> filter, 
            ProjectionDefinition<Employee> projection, 
            uint page, 
            uint pageSize)
        {
            var employeesProjection = _employeesCollection.Find(filter)
                .Project<BsonDocument>(projection)
                .Skip((int?)((page - 1) * pageSize))
                .Limit((int?)pageSize);

            return await employeesProjection.ToListAsync();
        }

        public async Task<bool> CheckIfEmployeeExists(string id)
        {
           FilterDefinition<Employee> filter = Builders<Employee>.Filter.Eq(e => e.Id, id); 
           var count = await _employeesCollection.CountDocumentsAsync(filter);
           
           return count > 0;
        }
    }

}
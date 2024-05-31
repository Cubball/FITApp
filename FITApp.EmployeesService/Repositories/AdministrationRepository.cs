using FITApp.EmployeesService.Interfaces;
using FITApp.EmployeesService.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace FITApp.EmployeesService.Repositories;

public class AdministrationRepository : IAdministrationRepository
{
    private readonly IMongoCollection<Administration> _collection;

    const string CollectionName = "administration";

    public AdministrationRepository(IOptions<MongoDbSettings> mongoDbSettings)
    {
        MongoClient client = new(mongoDbSettings.Value.ConnectionString);
        IMongoDatabase database = client.GetDatabase(mongoDbSettings.Value.DatabaseName);
        _collection = database.GetCollection<Administration>(CollectionName);
    }
    
    public async Task CreateAsync(Administration administration)
    {
        await _collection.InsertOneAsync(administration);
    }

    public async Task<Administration> GetAsync()
    {
        return await _collection.Find<Administration>(new BsonDocument()).FirstOrDefaultAsync();
    }

    public async Task UpdateAsync(Administration administration)
    {
        await _collection.UpdateOneAsync(new BsonDocument(), Builders<Administration>.Update
                                                                    .Set(a => a.HeadOfDepartment, administration.HeadOfDepartment)
                                                                    .Set(a => a.ScientificSecretary, administration.ScientificSecretary));
    }

}
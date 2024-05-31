using FITApp.PublicationsService.Interfaces;
using FITApp.PublicationsService.Models;
using MongoDB.Driver;

namespace FITApp.PublicationsService.Repositories
{
    public class AuthorRepository(IMongoDatabase database) : IAuthorRepository
    {
        const string CollectionName = "authors";
        private readonly IMongoCollection<Author> _collection = database.GetCollection<Author>(CollectionName);
        public async Task CreateAsync(Author author)
        {
            await _collection.InsertOneAsync(author);
        }

        public async Task CreateManyAsync(List<Author> authors)
        {
            await _collection.InsertManyAsync(authors);
        }

        public async Task DeleteAsync(string id)
        {
            await _collection.DeleteOneAsync<Author>(a => a.Id == id);
        }

        public async Task<Author> GetAsync(string id)
        {
            return await _collection.Find<Author>(a => a.Id == id).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(string id, Author author)
        {
            await _collection.UpdateOneAsync<Author>(a => a.Id == id, Builders<Author>.Update
                                                        .Set(a => a.FirstName, author.FirstName)
                                                        .Set(a => a.LastName, author.LastName)
                                                        .Set(a => a.Patronymic, author.Patronymic));
        }

        public async Task<IEnumerable<Author>> GetAllByIds(IEnumerable<string> ids)
        {
            return await _collection.Find<Author>(a => ids.Contains(a.Id)).ToListAsync();
        }

    }
}
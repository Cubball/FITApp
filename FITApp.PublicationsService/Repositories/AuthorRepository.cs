using FITApp.PublicationsService.Interfaces;
using FITApp.PublicationsService.Models;
using MongoDB.Driver;

namespace FITApp.PublicationsService.Repositories
{
    public class AuthorRepository(IMongoDatabase database) : IAuthorRepository
    {
        const string CollectionName = "authors";
        private readonly IMongoCollection<Author> _collection = database.GetCollection<Author>(CollectionName);
        public Task CreateAsync(Author author)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Author> GetAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(string id, Author author)
        {
            throw new NotImplementedException();
        }

    }
}
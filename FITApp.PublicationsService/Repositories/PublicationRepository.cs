using FITApp.PublicationsService.Interfaces;
using FITApp.PublicationsService.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace FITApp.PublicationsService.Repositories
{
    public class PublicationRepository(IMongoDatabase database) : IPublicationRepository
    {
        const string CollectionName = "publications";
        private readonly IMongoCollection<Publication> _collection = database.GetCollection<Publication>(CollectionName);
        public async Task CreateAsync(Publication publication)
        {
            await _collection.InsertOneAsync(publication);
        }

        public async Task DeleteAsync(ObjectId id)
        {
            await _collection.DeleteOneAsync<Publication>(p => p.Id == id);
        }

        public async Task<(IEnumerable<Publication>, int)> GetByAuthorAsync(string authorId, int pageNumber, int pageSize)
        {
            int total = (int)await _collection.CountDocumentsAsync<Publication>(p => p.AuthorId == authorId);
            var publications = await _collection.Find<Publication>(p => p.AuthorId == authorId)
            .Skip((pageNumber - 1) * pageSize)
            .Limit(pageSize)
            .ToListAsync();

            return (publications, total);
        }

        public async Task<IEnumerable<Publication>> GetBetweenDates(string authorId, DateOnly startDate, DateOnly endDate)
        {
            return await _collection.Find<Publication>(p => p.AuthorId == authorId && p.DateOfPublication > startDate && p.DateOfPublication < endDate).ToListAsync();
        }

        public async Task<Publication> GetByIdAsync(ObjectId id)
        {
            return await _collection.Find<Publication>(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(ObjectId id, Publication publication)
        {
            await _collection.UpdateOneAsync<Publication>(p => p.Id == id, Builders<Publication>.Update
                                                            .Set(p => p.Name, publication.Name)
                                                            .Set(p => p.Type, publication.Type)
                                                            .Set(p => p.Coauthors, publication.Coauthors)
                                                            .Set(p => p.Annotation, publication.Annotation)
                                                            .Set(p => p.EVersionLink, publication.EVersionLink)
                                                            .Set(p => p.PagesTotal, publication.PagesTotal)
                                                            .Set(p => p.PagesByAuthor, publication.PagesByAuthor)
                                                            .Set(p => p.DateOfPublication, publication.DateOfPublication));
        }

    }
}
using FITApp.PublicationsService.Models;
using MongoDB.Bson;

namespace FITApp.PublicationsService.Interfaces
{
    public interface IPublicationRepository
    {
        Task<Publication> GetByIdAsync(ObjectId id);

        Task<IEnumerable<Publication>> GetByAuthor(string authorId);

        Task Create(Publication publication);

        Task Update(ObjectId id, Publication publication);

        Task Delete(ObjectId id);
    }
}
using FITApp.PublicationsService.Models;
using MongoDB.Bson;

namespace FITApp.PublicationsService.Interfaces
{
    public interface IPublicationRepository
    {
        Task<Publication> GetByIdAsync(ObjectId id);

        Task<(IEnumerable<Publication>, int)> GetByAuthorAsync(string authorId, int pageNumber, int pageSize);

        Task CreateAsync(Publication publication);

        Task UpdateAsync(ObjectId id, Publication publication);

        Task DeleteAsync(ObjectId id);

        Task<IEnumerable<Publication>> GetBetweenDates(string authorId, DateOnly startDate, DateOnly endDate);
    }
}
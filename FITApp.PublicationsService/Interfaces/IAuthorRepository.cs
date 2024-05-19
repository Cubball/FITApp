using FITApp.PublicationsService.Models;

namespace FITApp.PublicationsService.Interfaces
{
    public interface IAuthorRepository
    {
        Task<Author> Get(string id);

        Task Create(Author author);

        Task Update(string id, Author author);

        Task Delete(string id);
    }
}
using FITApp.PublicationsService.Models;

namespace FITApp.PublicationsService.Interfaces
{
    public interface IAuthorRepository
    {
        Task<Author> GetAsync(string id);

        Task CreateAsync(Author author);

        Task UpdateAsync(string id, Author author);

        Task DeleteAsync(string id);

        Task CreateManyAsync(List<Author> authors);

        Task<IEnumerable<Author>> GetAllByIds(IEnumerable<string> ids);
    }
}
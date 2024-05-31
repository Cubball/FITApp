using FITApp.PublicationsService.Contracts;

namespace FITApp.PublicationsService.Interfaces
{
    public interface IAuthorService
    {
        Task UpdateAsync(string id, AuthorDTO authorDTO);

        Task DeleteAsync(string id);
    }
}
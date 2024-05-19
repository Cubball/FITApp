using FITApp.PublicationsService.Contracts.Requests;

namespace FITApp.PublicationsService.Interfaces
{
    public interface IAuthorService
    {
        Task UpdateAsync(string id, AuthorDTO authorDTO);

        Task DeleteAsync(string id);
    }
}
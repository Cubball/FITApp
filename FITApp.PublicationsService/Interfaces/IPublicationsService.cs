using FITApp.PublicationsService.Contracts.Requests;
using FITApp.PublicationsService.Contracts.Responses;

namespace FITApp.PublicationsService.Interfaces
{
    public interface IPublicationsService
    {
        Task CreateAsync(UpsertPublicationDTO publicationDTO, string userId);

        Task UpdateAsync(string id, UpsertPublicationDTO publicationDTO, string userId);

        Task DeleteAsync(string id, string userId);

        Task<FullPublication> GetById(string id, string userId);

        Task<AllPublicationsDTO> GetAll(string userId, int pageNumber, int pageSize);
    }
}
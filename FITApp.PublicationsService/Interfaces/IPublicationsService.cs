using FITApp.PublicationsService.Contracts.Requests;
using FITApp.PublicationsService.Contracts.Responses;

namespace FITApp.PublicationsService.Interfaces
{
    public interface IPublicationsService
    {
        Task CreateAsync(UpsertPublicationDTO publicationDTO, string userId);

        Task UpdateAsync(string id, UpsertPublicationDTO publicationDTO);

        Task DeleteAsync(string id);

        Task<FullPublication> GetById(string id);

        Task<AllPublicationsDTO> GetAll(string userId, int pageNumber, int pageSize);
    }
}
using FITApp.PublicationsService.Contracts;
using FITApp.PublicationsService.Helpers;
using FITApp.PublicationsService.Interfaces;

namespace FITApp.PublicationsService.Services
{
    public class AuthorService(IUnitOfWork unitOfWork) : IAuthorService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;


        public async Task DeleteAsync(string id)
        {
            await _unitOfWork.AuthorRepository.DeleteAsync(id);
        }

        public async Task UpdateAsync(string id, AuthorDTO authorDTO)
        {
            var author = await _unitOfWork.AuthorRepository.GetAsync(id);
            if (author == null)
            {
                return;
            }

            await _unitOfWork.AuthorRepository.UpdateAsync(id, authorDTO.Map());
        }

    }
}
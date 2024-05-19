using FITApp.PublicationsService.Contracts.Requests;
using FITApp.PublicationsService.Contracts.Responses;
using FITApp.PublicationsService.Exceptions;
using FITApp.PublicationsService.Helpers;
using FITApp.PublicationsService.Interfaces;
using FITApp.PublicationsService.Models;
using MongoDB.Bson;

namespace FITApp.PublicationsService.Services
{
    public class PublicationsService : IPublicationsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly HttpClient _httpClient;


        public PublicationsService(IUnitOfWork unitOfWork, HttpClient httpClient)
        {
            _unitOfWork = unitOfWork;
            _httpClient = httpClient;

        }

        public async Task CreateAsync(UpsertPublicationDTO publicationDTO, string userId)
        {
            var author = await _unitOfWork.AuthorRepository.GetAsync(userId);
            if (author == null)
            {
                Author response = await _httpClient.GetFromJsonAsync<Author>($"/api/employees/{userId}");
                await _unitOfWork.AuthorRepository.CreateAsync(response!);
            }

            Publication publication = publicationDTO.Map();

            await _unitOfWork.PublicationRepository.CreateAsync(publication);
        }

        public async Task DeleteAsync(string id, string userId)
        {
            var objectId = ObjectId.Parse(id);
            var publication = await _unitOfWork.PublicationRepository.GetByIdAsync(objectId);
            if (publication == null)
            {
                throw new NotFoundException("Publication not found");
            }

            if (publication.AuthorId != userId)
            {
                throw new NotAllowedException("You are not allowed to delete this publication");
            }

            await _unitOfWork.PublicationRepository.DeleteAsync(objectId);
        }

        public async Task<AllPublicationsDTO> GetAll(string userId, int pageNumber, int pageSize)
        {
            var publications = await _unitOfWork.PublicationRepository.GetByAuthorAsync(userId, pageNumber, pageSize);
            var result = new AllPublicationsDTO
            {
                Publications = publications.Item1.Select(p => p.MapToShortPublication()).ToList(),
                Total = publications.Item2,
                Page = pageNumber,
                PageSize = pageSize
            };

            return result;
        }

        public async Task<FullPublication> GetById(string id, string userId)
        {
            var publication = await _unitOfWork.PublicationRepository.GetByIdAsync(ObjectId.Parse(id));
            if (publication == null)
            {
                throw new NotFoundException("Publication not found");
            }

            if (publication.AuthorId != userId)
            {
                throw new NotAllowedException("You are not allowed to view this publication");
            }

            return publication.Map();
        }

        public async Task UpdateAsync(string id, UpsertPublicationDTO publicationDTO, string userId)
        {
            var objectId = ObjectId.Parse(id);
            var publicationCheck = await _unitOfWork.PublicationRepository.GetByIdAsync(objectId);
            if (publicationCheck == null)
            {
                throw new NotFoundException("Publication not found");
            }

            if (publicationCheck.AuthorId != userId)
            {
                throw new NotAllowedException("You are not allowed to update this publication");
            }

            var publication = publicationDTO.Map();
            await _unitOfWork.PublicationRepository.UpdateAsync(ObjectId.Parse(id), publication);
        }
    }
}
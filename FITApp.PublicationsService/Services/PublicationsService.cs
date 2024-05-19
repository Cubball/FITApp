using FITApp.PublicationsService.Contracts.Requests;
using FITApp.PublicationsService.Contracts.Responses;
using FITApp.PublicationsService.Helpers;
using FITApp.PublicationsService.Interfaces;
using FITApp.PublicationsService.Models;
using MongoDB.Bson;

namespace FITApp.PublicationsService.Services
{
    public class PublicationsService : IPublicationsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public PublicationsService(IUnitOfWork unitOfWork, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task CreateAsync(UpsertPublicationDTO publicationDTO, string userId)
        {
            var author = await _unitOfWork.AuthorRepository.GetAsync(userId);
            if (author == null)
            {
                var client = _httpClientFactory.CreateClient();
                client.BaseAddress = new Uri(_configuration["EmployeesServiceUrl"]!);
                Author response = await client.GetFromJsonAsync<Author>($"/{userId}");
                await _unitOfWork.AuthorRepository.CreateAsync(response!);
            }

            Publication publication = publicationDTO.Map();

            await _unitOfWork.PublicationRepository.CreateAsync(publication);
        }

        public async Task DeleteAsync(string id)
        {
            await _unitOfWork.PublicationRepository.DeleteAsync(ObjectId.Parse(id));
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

        public async Task<FullPublication> GetById(string id)
        {
            var publication = await _unitOfWork.PublicationRepository.GetByIdAsync(ObjectId.Parse(id));
            return publication.Map();
        }

        public async Task UpdateAsync(string id, UpsertPublicationDTO publicationDTO)
        {
            var publication = publicationDTO.Map();
            await _unitOfWork.PublicationRepository.UpdateAsync(ObjectId.Parse(id), publication);
        }
    }
}
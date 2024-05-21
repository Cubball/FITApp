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
                Author response = await _httpClient.GetFromJsonAsync<Author>($"/api/profile");
                await _unitOfWork.AuthorRepository.CreateAsync(response!);
            }

            await AddLackingAuthorsAsync(publicationDTO);

            Publication publication = publicationDTO.Map();
            publication.AuthorId = userId;

            await _unitOfWork.PublicationRepository.CreateAsync(publication);
        }

        public async Task DeleteAsync(string id, string userId)
        {
            var objectId = ObjectId.Parse(id);
            var publication =
                await _unitOfWork.PublicationRepository.GetByIdAsync(objectId)
                ?? throw new NotFoundException("Publication not found");

            if (publication.AuthorId != userId)
            {
                throw new NotAllowedException("You are not allowed to delete this publication");
            }

            await _unitOfWork.PublicationRepository.DeleteAsync(objectId);
        }

        public async Task<AllPublicationsDTO> GetAll(string userId, int pageNumber, int pageSize)
        {
            var publications = await _unitOfWork.PublicationRepository.GetByAuthorAsync(
                userId,
                pageNumber,
                pageSize
            );
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
            var publication =
                await _unitOfWork.PublicationRepository.GetByIdAsync(ObjectId.Parse(id))
                ?? throw new NotFoundException("Publication not found");

            if (publication.AuthorId != userId)
            {
                throw new NotAllowedException("You are not allowed to view this publication");
            }

            var result = publication.Map();

            await SetActualAuthors(result);

            return result;
        }

        public async Task UpdateAsync(string id, UpsertPublicationDTO publicationDTO, string userId)
        {
            var objectId = ObjectId.Parse(id);
            var publicationCheck =
                await _unitOfWork.PublicationRepository.GetByIdAsync(objectId)
                ?? throw new NotFoundException("Publication not found");

            if (publicationCheck.AuthorId != userId)
            {
                throw new NotAllowedException("You are not allowed to update this publication");
            }

            await AddLackingAuthorsAsync(publicationDTO);

            var publication = publicationDTO.Map();
            await _unitOfWork.PublicationRepository.UpdateAsync(ObjectId.Parse(id), publication);
        }

        public async Task<MemoryStream> GetReport(
            string userId,
            DateTime startDate,
            DateTime endDate
        )
        {
            var publications = await _unitOfWork.PublicationRepository.GetBetweenDates(
                userId,
                new DateOnly(startDate.Year, startDate.Month, startDate.Day),
                new DateOnly(endDate.Year, endDate.Month, endDate.Day)
            );
            var author = _httpClient.GetFromJsonAsync<Author>("/api/profile");

            var dtos = publications.Select(async p =>
            {
                var dto = p.Map();
                await SetActualAuthors(dto);
                return dto;
            });

            return default;
        }

        private async Task AddLackingAuthorsAsync(UpsertPublicationDTO publicationDTO)
        {
            foreach (var coauthor in publicationDTO.Coauthors)
            {
                if (coauthor.Id != null)
                {
                    var coauthorCheck = _unitOfWork.AuthorRepository.GetAsync(coauthor.Id);
                    if (coauthorCheck == null)
                    {
                        await _unitOfWork.AuthorRepository.CreateAsync(coauthor.Map());
                    }
                }
            }
        }

        private async Task SetActualAuthors(FullPublication fullPublication)
        {
            for (int i = 0; i < fullPublication.Coauthors.Count; i++)
            {
                if (fullPublication.Coauthors[i].Id != null)
                {
                    var author = await _unitOfWork.AuthorRepository.GetAsync(
                        fullPublication.Coauthors[i].Id
                    );
                    fullPublication.Coauthors[i] = author.MapToCoauthor();
                }
            }
        }
    }
}

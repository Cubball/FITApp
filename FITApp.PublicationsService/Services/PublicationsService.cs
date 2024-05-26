using FITApp.PublicationsService.Contracts.Requests;
using FITApp.PublicationsService.Contracts.Responses;
using FITApp.PublicationsService.Documents;
using FITApp.PublicationsService.Exceptions;
using FITApp.PublicationsService.Helpers;
using FITApp.PublicationsService.Interfaces;
using FITApp.PublicationsService.Models;
using MongoDB.Bson;
using QuestPDF.Fluent;

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
            var response = await _unitOfWork.AuthorRepository.GetAsync(userId);
            if (response == null)
            {
                response = await _httpClient.GetFromJsonAsync<Author>($"/api/profile");
                await _unitOfWork.AuthorRepository.CreateAsync(response!);
            }

            await AddLackingAuthorsAsync(publicationDTO);

            Publication publication = publicationDTO.Map();
            var publicationAuthor = response.MapToPublicationAuthor();
            publicationAuthor.PagesByAuthor = publicationDTO.PagesByAuthorCount;
            publication.Authors.Add(publicationAuthor);

            await _unitOfWork.PublicationRepository.CreateAsync(publication);
        }

        public async Task DeleteAsync(string id, string userId)
        {
            var objectId = ObjectId.Parse(id);
            var publication =
                await _unitOfWork.PublicationRepository.GetByIdAsync(objectId)
                ?? throw new NotFoundException("Publication not found");

            if (!publication.Authors.Select(x => x.Id).Contains(userId))
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

            if (!publication.Authors.Select(x => x.Id).Contains(userId))
            {
                throw new NotAllowedException("You are not allowed to view this publication");
            }

            var result = publication.Map();

            // await SetActualAuthors(result);

            return result;
        }

        public async Task UpdateAsync(string id, UpsertPublicationDTO publicationDTO, string userId)
        {
            var objectId = ObjectId.Parse(id);
            var publicationCheck =
                await _unitOfWork.PublicationRepository.GetByIdAsync(objectId)
                ?? throw new NotFoundException("Publication not found");

            if (!publicationCheck.Authors.Select(x => x.Id).Contains(userId))
            {
                throw new NotAllowedException("You are not allowed to update this publication");
            }

            await AddLackingAuthorsAsync(publicationDTO);

            var publication = publicationDTO.Map();
            var author = await _unitOfWork.AuthorRepository.GetAsync(userId);
            var publicationAuthor = author.MapToPublicationAuthor();
            publicationAuthor.PagesByAuthor = publicationDTO.PagesByAuthorCount;
            publication.Authors.Add(publicationAuthor);
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
            var author = await _httpClient.GetFromJsonAsync<Author>("/api/profile");

            var dtos = publications.Select(p => p.Map());



            var administration = await _httpClient.GetFromJsonAsync<Administration>("api/administration");

            var document = new Report(dtos, author, administration);

            var bytes = document.GeneratePdf();

            var ms = new MemoryStream(bytes);

            return ms;
        }

        // private async Task AddLackingAuthorsAsync(UpsertPublicationDTO publicationDTO)
        // {
        //     foreach (var coauthor in publicationDTO.Authors)
        //     {
        //         if (coauthor.Id != null)
        //         {
        //             var coauthorCheck = await _unitOfWork.AuthorRepository.GetAsync(coauthor.Id);
        //             if (coauthorCheck == null)
        //             {
        //                 await _unitOfWork.AuthorRepository.CreateAsync(coauthor.Map());
        //             }
        //         }
        //     }
        // }

        private async Task AddLackingAuthorsAsync(UpsertPublicationDTO publicationDTO)
        {
            var authorIds = publicationDTO.Authors.Select(p => p.Id).Where(id => !string.IsNullOrWhiteSpace(id));
            var authors = await _unitOfWork.AuthorRepository.GetAllByIds(authorIds);
            var authorsDict = authors.ToDictionary(a => a.Id);
            var authorsToAdd = new List<Author>();
            foreach (var author in publicationDTO.Authors)
            {
                if (author.Id is not null && !authorsDict.ContainsKey(author.Id))
                {
                    authorsToAdd.Add(author.Map());
                }
            }

            if (authorsToAdd.Count > 0)
            {
                await _unitOfWork.AuthorRepository.CreateManyAsync(authorsToAdd);
            }
        }

        private async Task SetActualAuthors(FullPublication fullPublication)
        {
            for (int i = 0; i < fullPublication.Authors.Count; i++)
            {
                if (fullPublication.Authors[i].Id != null)
                {
                    var author = await _unitOfWork.AuthorRepository.GetAsync(
                        fullPublication.Authors[i].Id
                    );
                    fullPublication.Authors[i] = author.MapToCoauthor();
                }
            }
        }
    }
}

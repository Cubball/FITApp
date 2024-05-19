using FITApp.PublicationsService.Contracts;
using FITApp.PublicationsService.Contracts.Requests;
using FITApp.PublicationsService.Contracts.Responses;
using FITApp.PublicationsService.Models;

namespace FITApp.PublicationsService.Helpers
{
    public static class Extensions
    {
        public static Publication Map(this UpsertPublicationDTO publicationDTO)
        {
            var publication = new Publication();
            publication.Name = publicationDTO.Name;
            publication.Type = publicationDTO.Type;
            publication.Coauthors = publicationDTO.Coauthors.Select(c => new Coauthor
            {
                AuthorId = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Patronymic = c.Patronymic
            }).ToList();
            publication.Annotation = publicationDTO.Annotation;
            publication.EVersionLink = publicationDTO.EVersionLink;
            publication.PagesTotal = publicationDTO.PagesCount;
            publication.PagesByAuthor = publicationDTO.PagesByAuthorCount;
            publication.DateOfPublication = new DateOnly(publicationDTO.DateOfPublication.Year, publicationDTO.DateOfPublication.Month, publicationDTO.DateOfPublication.Day);

            return publication;
        }

        public static FullPublication Map(this Publication publication)
        {
            return new FullPublication
            {
                Id = publication.Id.ToString(),
                Name = publication.Name,
                Type = publication.Type,
                Coauthors = publication.Coauthors.Select(c => new CoauthorDTO
                {
                    Id = c.AuthorId,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    Patronymic = c.Patronymic
                }).ToList(),
                Annotation = publication.Annotation,
                EVersionLink = publication.EVersionLink,
                PagesCount = publication.PagesTotal,
                PagesByAuthorCount = publication.PagesByAuthor,
                DateOfPublication = publication.DateOfPublication
            };
        }

        public static ShortPublication MapToShortPublication(this Publication publication)
        {
            return new ShortPublication
            {
                Id = publication.Id.ToString(),
                Name = publication.Name,
                Type = publication.Type,
                DateOfPublication = publication.DateOfPublication
            };
        }

        public static Author Map(this AuthorDTO authorDTO)
        {
            return new Author
            {
                FirstName = authorDTO.FirstName,
                LastName = authorDTO.LastName,
                Patronymic = authorDTO.Patronymic
            };
        }
    }
}
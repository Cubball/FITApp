using FITApp.PublicationsService.Contracts;
using FITApp.PublicationsService.Contracts.Requests;
using FITApp.PublicationsService.Contracts.Responses;
using FITApp.PublicationsService.Models;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

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

        public static string GetUserId(this ControllerBase controller)
        {
            return controller.User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
        }

        public static bool Validate(this UpsertPublicationDTO publicationDTO)
        {
            return publicationDTO != null
                && !string.IsNullOrWhiteSpace(publicationDTO.Name)
                && !string.IsNullOrWhiteSpace(publicationDTO.Type)
                && !string.IsNullOrWhiteSpace(publicationDTO.Annotation)
                && !string.IsNullOrWhiteSpace(publicationDTO.EVersionLink)
                && publicationDTO.DateOfPublication < DateTime.Now
                && publicationDTO.DateOfPublication != default
                && publicationDTO.PagesCount > 0
                && publicationDTO.PagesByAuthorCount > 0
                && publicationDTO.PagesByAuthorCount <= publicationDTO.PagesCount;
        }

        public static bool Validate(this AuthorDTO authorDTO)
        {
            return authorDTO != null
                && !string.IsNullOrWhiteSpace(authorDTO.FirstName)
                && !string.IsNullOrWhiteSpace(authorDTO.LastName)
                && !string.IsNullOrWhiteSpace(authorDTO.Patronymic);
        }
    }
}
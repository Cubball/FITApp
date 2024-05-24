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
            publication.Authors = publicationDTO.Authors.Select(c => new Author
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Patronymic = c.Patronymic,
                PagesByAuthor = c.PagesByAuthorCount
            }).ToList();
            publication.Annotation = publicationDTO.Annotation;
            publication.EVersionLink = publicationDTO.EVersionLink;
            publication.PagesTotal = publicationDTO.PagesCount;
            publication.DateOfPublication = new DateOnly(publicationDTO.DateOfPublication.Year, publicationDTO.DateOfPublication.Month, publicationDTO.DateOfPublication.Day);
            publication.InputData = publicationDTO.InputData;

            return publication;
        }

        public static FullPublication Map(this Publication publication)
        {
            return new FullPublication
            {
                Id = publication.Id.ToString(),
                Name = publication.Name,
                Type = publication.Type,
                Authors = publication.Authors.Select(c => new AuthorWithPagesDTO
                {
                    Id = c.Id,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    Patronymic = c.Patronymic,
                    PagesByAuthorCount = c.PagesByAuthor,
                }).ToList(),
                Annotation = publication.Annotation,
                EVersionLink = publication.EVersionLink,
                PagesCount = publication.PagesTotal,
                DateOfPublication = publication.DateOfPublication,
                InputData = publication.InputData,
            };
        }

        public static ShortPublication MapToShortPublication(this Publication publication)
        {
            return new ShortPublication
            {
                Id = publication.Id.ToString(),
                Name = publication.Name,
                Type = publication.Type,
                DateOfPublication = publication.DateOfPublication,
                MainAuthor = publication.Authors.OrderByDescending(a => a.PagesByAuthor).First().Map()
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

        public static AuthorDTO Map(this Author author)
        {
            return new AuthorDTO
            {
                FirstName = author.FirstName,
                LastName = author.LastName,
                Patronymic = author.Patronymic
            };
        }

        public static Author Map(this AuthorWithPagesDTO coauthorDTO)
        {
            return new Author {
                Id = coauthorDTO.Id,
                FirstName = coauthorDTO.FirstName,
                LastName = coauthorDTO.LastName,
                Patronymic = coauthorDTO.Patronymic,
            };
        }

        public static AuthorWithPagesDTO MapToCoauthor(this Author author)
        {
            return new AuthorWithPagesDTO {
                Id = author.Id,
                FirstName = author.FirstName,
                LastName = author.LastName,
                Patronymic = author.Patronymic,
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
                && publicationDTO.Authors.Sum(a => a.PagesByAuthorCount) + publicationDTO.PagesByAuthorCount < publicationDTO.PagesCount;
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
using FITApp.PublicationsService.Interfaces;
using Microsoft.AspNetCore.WebUtilities;

namespace FITApp.PublicationsService.Repositories
{
    public class UnitOfWork(IPublicationRepository publicationRepository, IAuthorRepository authorRepository) : IUnitOfWork
    {
        private readonly IPublicationRepository _publicationRepository = publicationRepository;

        private readonly IAuthorRepository _authorRepository = authorRepository;

        public IPublicationRepository PublicationRepository { get => _publicationRepository; }
        public IAuthorRepository AuthorRepository { get => _authorRepository; }

    }
}
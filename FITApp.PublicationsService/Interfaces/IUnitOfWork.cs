namespace FITApp.PublicationsService.Interfaces
{
    public interface IUnitOfWork
    {
        public IPublicationRepository PublicationRepository { get; }

        public IAuthorRepository AuthorRepository { get; }
    }
}
namespace FITApp.PublicationsService.Contracts.Requests
{
    public class UpsertPublicationDTO
    {
        public string Name { get; set; }

        public string Type { get; set; }

        public string Annotation { get; set; }

        public string EVersionLink { get; set; }

        public string InputData { get; set; }

        public DateTime DateOfPublication { get; set; }

        public int PagesCount { get; set; }

        public int PagesByAuthorCount { get; set; }

        public ICollection<AuthorWithPagesDTO> Authors { get; set; }
    }
}
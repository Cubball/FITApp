namespace FITApp.PublicationsService.Contracts.Responses
{
    public class FullPublication
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public string Annotation { get; set; }

        public string EVersionLink { get; set; }

        public DateOnly DateOfPublication { get; set; }

        public int PagesCount { get; set; }

        public int PagesByAuthorCount { get; set; }

        public ICollection<CoauthorDTO> Coauthors { get; set; }

    }
}
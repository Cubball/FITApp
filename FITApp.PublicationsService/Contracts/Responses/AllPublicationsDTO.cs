namespace FITApp.PublicationsService.Contracts.Responses
{
    public class AllPublicationsDTO
    {
        public int Page { get; set; }

        public int PageSize { get; set; }

        public int Total { get; set; }

        public ICollection<ShortPublication> Publications { get; set; }
    }
}
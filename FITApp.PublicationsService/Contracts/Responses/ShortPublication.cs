namespace FITApp.PublicationsService.Contracts.Responses
{
    public class ShortPublication
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public DateOnly DateOfPublication { get; set; }
    }
}
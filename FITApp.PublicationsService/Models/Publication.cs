using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FITApp.PublicationsService.Models
{
    public class Publication
{
    public ObjectId Id { get; set; } // or string/guid

    public string Name { get; set; } = null!;

    public string Type { get; set; }

    public IList<PublicationAuthor> Authors { get; set; } = null!;

    public string Annotation { get; set; } = null!;

    public string EVersionLink { get; set; } = null!;

    public string InputData { get; set; }

    public int PagesTotal { get; set; }

    public DateOnly DateOfPublication { get; set; }
}
}
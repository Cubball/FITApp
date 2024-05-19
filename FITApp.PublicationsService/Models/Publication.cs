using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FITApp.PublicationsService.Models
{
    public class Publication
{
    public ObjectId Id { get; set; } // or string/guid

    public string Name { get; set; } = null!;

    public string Type { get; set; }

    public string AuthorId { get; set; } = null!;

    // This is for joining later
    [BsonIgnore]
    public Author Author { get; set; } = null!;

    public ICollection<Coauthor> Coauthors { get; set; } = null!;

    public string Annotation { get; set; } = null!;

    public string EVersionLink { get; set; } = null!;

    public int PagesTotal { get; set; }

    public int PagesByAuthor { get; set; }

    public DateOnly DateOfPublication { get; set; }
}
}
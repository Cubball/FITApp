using MongoDB.Bson.Serialization.Attributes;

namespace FITApp.PublicationsService.Models
{
    public class Coauthor
{
    public string? AuthorId { get; set; }

    // Maybe for joining, or maybe remove
    [BsonIgnore]
    public Author? Author { get; set; }

    public string? FirstName { get; set; } = null!;

    public string? LastName { get; set; } = null!;

    public string? Patronymic { get; set; } = null!;
}
}
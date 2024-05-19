using MongoDB.Bson.Serialization.Attributes;

namespace FITApp.PublicationsService.Models
{
    public class Author
{
    [BsonId]
    public string Id { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Patronymic { get; set; } = null!;
}
}
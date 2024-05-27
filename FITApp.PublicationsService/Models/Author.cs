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

        [BsonIgnore]
        public string FirstNamePossessive { get; set; } = null!;

        [BsonIgnore]
        public string LastNamePossessive { get; set; } = null!;

        [BsonIgnore]
        public string PatronymicPossessive { get; set; } = null!;

        [BsonIgnore]
        public ICollection<AcademicDegree> AcademicDegrees { get; set; }

        [BsonIgnore]
        public ICollection<Position> Positions { get; set; }
    }
}

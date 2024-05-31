using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using ThirdParty.Json.LitJson;
namespace FITApp.EmployeesService.Models
{
    public class Employee
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public string Id { get; set; }
        public User User { get; set; }
        [BsonIgnoreIfNull]
        [BsonDefaultValue(null)]
        public string? FirstName { get; set; }
        [BsonIgnoreIfNull]
        [BsonDefaultValue(null)]
        public string? LastName { get; set; }
        [BsonIgnoreIfNull]
        [BsonDefaultValue(null)]
        public string? Patronymic { get; set; }
        [BsonIgnoreIfNull]
        [BsonDefaultValue(null)]
        public string? FirstNamePossessive { get; set; }
        [BsonIgnoreIfNull]
        [BsonDefaultValue(null)]
        public string? LastNamePossessive { get; set; }
        [BsonIgnoreIfNull]
        [BsonDefaultValue(null)]
        public string? PatronymicPossessive { get; set; }
        [BsonIgnoreIfDefault]
        public DateOnly? BirthDate { get; set; }
        [BsonIgnoreIfNull]
        [BsonDefaultValue(null)]
        public string? Photo { get; set; }
        [BsonIgnoreIfNull]
        public List<Position> Positions { get; set; }
        [BsonIgnoreIfNull]
        public List<Education> Educations { get; set; }
        [BsonIgnoreIfNull]
        public List<AcademicDegree> AcademicDegrees { get; set; }
        [BsonIgnoreIfNull]
        public List<AcademicRank> AcademicRanks { get; set; }
    }
}
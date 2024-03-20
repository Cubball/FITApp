using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace FITApp.EmployeesService.Models
{
    public class Employee
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        public User User { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public DateOnly BirthDate { get; set; }
        public string Photo { get; set; }
        public List<Position> Positions { get; set; } = [];
        public List<Education> Educations { get; set; } = [];
        public List<AcademicDegree> AcademicDegrees { get; set; } = [];
        public List<AcademicRank> AcademicRanks { get; set; } = [];
    }
}
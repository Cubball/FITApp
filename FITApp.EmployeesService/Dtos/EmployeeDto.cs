using FITApp.EmployeesService.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FITApp.EmployeesService.Dtos
{

    // public class EmployeeDto
    // {
    //     public string Id { get; set; }
    //     public UserDto User { get; set; }
    //     public string FirstName { get; set; }
    //     public string LastName { get; set; }
    //     public string Patronymic { get; set; }
    //     public DateTime BirthDate { get; set; }
    //     public string Photo { get; set; }
    //     public List<PositionDto> Positions { get; set; } = [];
    //     public List<EducationDto> Educations { get; set; } = [];
    //     public List<AcademicDegreeDto> AcademicDegrees { get; set; } = [];
    //     public List<AcademicRankDto> AcademicRanks { get; set; } = [];
    // }
    public class EmployeeDto
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
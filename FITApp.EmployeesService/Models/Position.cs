using MongoDB.Bson.Serialization.Attributes;

namespace FITApp.EmployeesService.Models
{
    public class Position
    {
        public string Name { get; set; }
        [BsonIgnoreIfDefault]
        public DateOnly? StartDate { get; set; }
        [BsonIgnoreIfDefault]
        public DateOnly? EndDate { get; set; }

    }

}
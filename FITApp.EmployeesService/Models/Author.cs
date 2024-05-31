using MongoDB.Bson.Serialization.Attributes;

namespace FITApp.EmployeesService.Models
{
    public class Author
    {
        public string? Id { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Patronymic { get; set; } = null!;
    }
}

namespace FITApp.EmployeesService.Dtos
{
    public class AuthorDto
    {
        public string? Id { get; set; } = null!;

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Patronymic { get; set; } = string.Empty;
    }
}
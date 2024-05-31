namespace FITApp.PublicationsService.Contracts;

public class AuthorWithPagesDTO
{
    public string? Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Patronymic { get; set; }

    public int? PagesByAuthorCount { get; set; }
}
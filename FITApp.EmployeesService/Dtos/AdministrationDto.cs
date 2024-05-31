namespace FITApp.EmployeesService.Dtos
{
    public class AdministrationDto
    {
        public AuthorDto HeadOfDepartment { get; set; } = new AuthorDto();

        public AuthorDto ScientificSecretary { get; set; } = new AuthorDto();
    }
}
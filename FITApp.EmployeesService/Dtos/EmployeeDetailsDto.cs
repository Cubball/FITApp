namespace FITApp.EmployeesService.Dtos
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class EmployeeDetailsDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        [Required(ErrorMessage = "BirthDate is required")]
        public DateTime? BirthDate { get; set; }
    }
}
namespace FITApp.EmployeesService.Dtos
{
    using System;

    public class EmployeeDetailsDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
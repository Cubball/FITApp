namespace FITApp.EmployeesService.Dtos
{
    using System;

    public class AcademicDegreeDto
    {
        public string FullName { get; set; }
        public string ShortName { get; set; }
        public string DeplomanNumber { get; set; }
        public DateTime DateOfIssue { get; set; }
    }
}
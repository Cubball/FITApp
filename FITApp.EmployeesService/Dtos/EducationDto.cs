using System.ComponentModel.DataAnnotations;

namespace FITApp.EmployeesService.Dtos
{
    using System;

    public class EducationDto
    {
        public string University { get; set; }
        public string Specialization { get; set; }
        [Required]
        public DateTime? DiplomaDateOfIssue { get; set; }
    }
}
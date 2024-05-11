using System.ComponentModel.DataAnnotations;

namespace FITApp.EmployeesService.Dtos
{
    using System;

    public class AcademicDegreeDto
    {
        public string FullName { get; set; }
        public string ShortName { get; set; }
        public string DiplomaNumber { get; set; }
        [Required]
        public DateTime? DateOfIssue { get; set; }
    }
}
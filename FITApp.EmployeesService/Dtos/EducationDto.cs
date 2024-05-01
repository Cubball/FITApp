using System.ComponentModel.DataAnnotations;

namespace FITApp.EmployeesService.Dtos
{
    public class EducationDto
    {
        public string University { get; set; }
        public string Specialization { get; set; }
        [Required]
        public DateTime? DiplomaDateOfIssue { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace FITApp.EmployeesService.Dtos
{
    using System;

    public class AcademicRankDto
    {
        public string Name { get; set; }
        public string CertificateNumber { get; set; }
        [Required]
        public DateTime? DateOfIssue { get; set; }
    }
}
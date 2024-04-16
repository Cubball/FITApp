using System.ComponentModel.DataAnnotations;

namespace FITApp.EmployeesService.Dtos
{
    using System;

    public class PositionDto
    {
        public string Name { get; set; }
        [Required]
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
namespace FITApp.EmployeesService.Dtos
{
    using System;

    public class PositionDto
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
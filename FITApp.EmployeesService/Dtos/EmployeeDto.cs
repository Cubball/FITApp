namespace FITApp.EmployeesService.Dtos
{

    public class EmployeeDto
    {
        public string Id { get; set; }
        public UserDto User { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public DateTime BirthDate { get; set; }
        public string Photo { get; set; }
        public List<PositionDto> Positions { get; set; } = [];
        public List<EducationDto> Educations { get; set; } = [];
        public List<AcademicDegreeDto> AcademicDegrees { get; set; } = [];
        public List<AcademicRankDto> AcademicRanks { get; set; } = [];
    }
}
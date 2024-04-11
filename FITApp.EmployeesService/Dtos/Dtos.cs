namespace FITApp.EmployeesService.Dtos
{
    using System;
    using System.Collections.Generic;

    public class EmployeeDto
    {
        public string Id { get; set; }
        public UserDto User { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public DateTime BirthDate { get; set; }
        public string Photo { get; set; }
        public List<PositionDto> Positions { get; set; } = new List<PositionDto>();
        public List<EducationDto> Educations { get; set; } = new List<EducationDto>();
        public List<AcademicDegreeDto> AcademicDegrees { get; set; } = new List<AcademicDegreeDto>();
        public List<AcademicRankDto> AcademicRanks { get; set; } = new List<AcademicRankDto>();
    }
    public class EmployeeDetailsDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public DateTime BirthDate { get; set; }
    }

    public class UserDto
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }

    public class PositionDto
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }

    public class EducationDto
    {
        public string University { get; set; }
        public string Specialization { get; set; }
        public DateTime DiplomaDateOfIssue { get; set; }
    }

    public class AcademicDegreeDto
    {
        public string FullName { get; set; }
        public string ShortName { get; set; }
        public string DeplomanNumber { get; set; }
        public DateTime DateOfIssue { get; set; }
    }

    public class AcademicRankDto
    {
        public string Name { get; set; }
        public string SerteficateNumber { get; set; }
        public DateTime DateOfIssue { get; set; }
    }
}
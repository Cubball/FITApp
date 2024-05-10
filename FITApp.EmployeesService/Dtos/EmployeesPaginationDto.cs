namespace FITApp.EmployeesService.Dtos;

public class EmployeesPaginationDto
{
    public uint Page { get; set; }
    public uint PageSize { get; set; }
    public long TotalCount { get; set; }
    public List<SimpleEmployeeDto> Employees { get; set; }
}
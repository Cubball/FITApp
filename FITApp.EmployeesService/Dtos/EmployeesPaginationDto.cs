namespace FITApp.EmployeesService.Dtos;

public class EmployeesPaginationDto
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public long TotalCount { get; set; }
    public IEnumerable<SimpleEmployeeDto> Employees { get; set; }
}
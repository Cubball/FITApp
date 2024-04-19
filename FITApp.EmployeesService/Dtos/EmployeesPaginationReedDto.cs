namespace FITApp.EmployeesService.Dtos;

public class EmployeesPaginationReedDto
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public IEnumerable<EmployeeDto> Employees { get; set; }
}
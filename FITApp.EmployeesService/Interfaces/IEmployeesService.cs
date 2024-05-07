using FITApp.EmployeesService.Dtos;
using FITApp.EmployeesService.Models;

namespace FITApp.EmployeesService.Interfaces
{
    public interface IEmployeesService
    {
        Task<IEnumerable<Employee>> GetEmployees();
        Task<Employee> GetEmployee(string id);
        Task CreateEmployee(EmployeeDto employeeDto);
        Task<long> DeleteEmployee(string id);
        Task<long> UpdateEmployeeDetails(string id, EmployeeDetailsDto employeeDetails);
        Task<long> UpdateEmployeePositions(string id, PositionDto positionDto);
        Task<long> UpdateEmployeeEducations(string id, EducationDto educationDto);
        Task<long> UpdateEmployeeAcademicDegrees(string id, AcademicDegreeDto academicDegreeDto);
        Task<long> UpdateEmployeeAcademicRanks(string id, AcademicRankDto academicRankDto);
        Task<long> RemoveEmployeeAcademicRankByIndex(string id, int index);
        Task<long> RemoveEmployeePositionByIndex(string id, int index);
        Task<long> RemoveEmployeeEducationByIndex(string id, int index);
        Task<long> RemoveEmployeeAcademicDegreeByIndex(string id, int index);
        Task<EmployeesPaginationDto> GetEmployeesPagination(uint page, uint pageSize);
    }
}
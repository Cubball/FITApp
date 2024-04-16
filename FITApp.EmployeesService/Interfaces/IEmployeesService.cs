using FITApp.EmployeesService.Dtos;
using FITApp.EmployeesService.Models;
using MongoDB.Driver;

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
        Task<long> UpdateEmployeeEducations(string id, EducationDto positionDto);
    }
}
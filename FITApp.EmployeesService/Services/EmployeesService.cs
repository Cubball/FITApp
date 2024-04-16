using FITApp.EmployeesService.Dtos;
using FITApp.EmployeesService.Interfaces;
using FITApp.EmployeesService.Models;
using MongoDB.Driver;

namespace FITApp.EmployeesService.Services
{

    public class EmployeesService(IEmployeesRepository employeeRepository) : IEmployeesService
    {
        public async Task CreateEmployee(Employee employee)
        {
            await employeeRepository.CreateEmployee(employee);
        }

        public async Task<long> DeleteEmployee(string id)
        {
            var result = await employeeRepository.DeleteEmployee(id);
            return result.DeletedCount;
        }

        public async Task<Employee> GetEmployee(string id)
        {
            return await employeeRepository.GetEmployee(id);
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await employeeRepository.GetEmployees();
        }

        public async Task<long> UpdateEmployeeDetails(string id, EmployeeDetailsDto employeeDetails)
        {
            DateOnly newDateFromDateTime = new(employeeDetails.BirthDate!.Value.Year,
                                   employeeDetails.BirthDate.Value.Month,
                                   employeeDetails.BirthDate.Value.Day);
            
            UpdateDefinition<Employee> update = Builders<Employee>.Update
                .Set(employee => employee.FirstName, employeeDetails.FirstName)
                .Set(employee => employee.LastName, employeeDetails.LastName)
                .Set(employee => employee.Patronymic, employeeDetails.Patronymic)
                .Set(employee => employee.BirthDate, newDateFromDateTime);
            var result = await employeeRepository.UpdateEmployee(id, update);
            return result.ModifiedCount;
        }
        
        public async Task<long> UpdateEmployeePositions(string id, PositionDto positionDto)
        {
            DateOnly startDate = new(positionDto.StartDate.Year,
                positionDto.StartDate.Month,
                positionDto.StartDate.Day);
            
            UpdateDefinition<Employee> update;

            if (positionDto.EndDate.HasValue)
            {
                DateOnly endDate = new(positionDto.EndDate.Value.Year,
                    positionDto.EndDate.Value.Month,
                    positionDto.EndDate.Value.Day);

                update = Builders<Employee>.Update.Push(e => e.Positions, new Position
                {
                    Name = positionDto.Name,
                    StartDate = startDate,
                    EndDate = endDate
                });
            }
            else
            {
                update = Builders<Employee>.Update.Push(e => e.Positions, new Position
                {
                    Name = positionDto.Name,
                    StartDate = startDate
                });
            }

            var result = await employeeRepository.UpdateEmployee(id, update);
            return result.ModifiedCount;
        }

    }
}
using AutoMapper;
using FITApp.EmployeesService.Dtos;
using FITApp.EmployeesService.Interfaces;
using FITApp.EmployeesService.Models;
using FluentValidation;
using MongoDB.Driver;

namespace FITApp.EmployeesService.Services
{
    public class EmployeesService(IEmployeesRepository employeeRepository,
                                  IMapper mapper,
                                  IValidator<PositionDto> positionValidator,
                                  IValidator<EducationDto> educationValidator,
                                  IValidator<AcademicDegreeDto> academicDegreeDtoValidator,
                                  IValidator<AcademicRankDto> academicRankDtoValidator) : IEmployeesService
    {
        public async Task CreateEmployee(EmployeeDto employeeDto)
        {
            Employee employee = mapper.Map<Employee>(employeeDto);
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
            var validationResult = await positionValidator.ValidateAsync(positionDto);
            // Check if the validation failed
            if (!validationResult.IsValid)
            {
                throw new ValidationException("PositionDto validation failed.", validationResult.Errors);
            }
            var position = mapper.Map<Position>(positionDto);

            var update = Builders<Employee>.Update.Push(e => e.Positions, new Position
            {
                Name = position.Name,
                StartDate = position.StartDate,
                EndDate = position.EndDate
            });


            var result = await employeeRepository.UpdateEmployee(id, update);
            return result.ModifiedCount;
        }

        public async Task<long> UpdateEmployeeEducations(string id, EducationDto educationDto)
        {
            var validationResult = await educationValidator.ValidateAsync(educationDto);
            // Check if the validation failed
            if (!validationResult.IsValid)
            {
                throw new ValidationException("EducationDto validation failed.", validationResult.Errors);
            }
            var education = mapper.Map<Education>(educationDto);

            var update = Builders<Employee>.Update.Push(e => e.Educations, new Education
            {
                University = education.University,
                Specialization = education.Specialization,
                DiplomaDateOfIssue = education.DiplomaDateOfIssue
            });

            var result = await employeeRepository.UpdateEmployee(id, update);
            return result.ModifiedCount;
        }

        public async Task<long> UpdateEmployeeAcademicDegrees(string id, AcademicDegreeDto academicDegreeDto)
        {
            var validationResult = await academicDegreeDtoValidator.ValidateAsync(academicDegreeDto);

            if (!validationResult.IsValid)
            {
                throw new ValidationException("AcademicDegreeDto validation failed.", validationResult.Errors);
            }
            var academicDegree = mapper.Map<AcademicDegree>(academicDegreeDto);

            var update = Builders<Employee>.Update.Push(e => e.AcademicDegrees, new AcademicDegree
            {
                FullName = academicDegree.FullName,
                ShortName = academicDegree.ShortName,
                DiplomaNumber = academicDegree.DiplomaNumber,
                DateOfIssue = academicDegree.DateOfIssue
            });

            var result = await employeeRepository.UpdateEmployee(id, update);
            return result.ModifiedCount;
        }

        public async Task<long> UpdateEmployeeAcademicRanks(string id, AcademicRankDto academicRankDto)
        {
            var validationResult = await academicRankDtoValidator.ValidateAsync(academicRankDto);
            if (!validationResult.IsValid)
            {
                throw new ValidationException("AcademicRankDto validation failed.", validationResult.Errors);
            }

            var academicDegree = mapper.Map<AcademicRank>(academicRankDto);

            var update = Builders<Employee>.Update.Push(e => e.AcademicRanks, new AcademicRank
            {
                Name = academicDegree.Name,
                CertificateNumber = academicDegree.CertificateNumber,
                DateOfIssue = academicDegree.DateOfIssue
            });

            var result = await employeeRepository.UpdateEmployee(id, update);
            return result.ModifiedCount;
        }


        public async Task<long> RemoveEmployeeAcademicRankByIndex(string id, int index)
        {
            var result = await employeeRepository.RemoveArrayElementByIndex<AcademicRank>(id,
                                        index,
                                        employee => employee.AcademicRanks,
                                        e => e.AcademicRanks);
            return result.ModifiedCount;
        }

        public async Task<long> RemoveEmployeePositionByIndex(string id, int index)
        {
            var result = await employeeRepository.RemoveArrayElementByIndex<Position>(id,
                                        index,
                                        employee => employee.Positions,
                                        e => e.Positions);
            return result.ModifiedCount;
        }

        public async Task<long> RemoveEmployeeEducationByIndex(string id, int index)
        {
            var result = await employeeRepository.RemoveArrayElementByIndex<Education>(id,
                                        index,
                                        employee => employee.Educations,
                                        e => e.Educations);
            return result.ModifiedCount;
        }

        public async Task<long> RemoveEmployeeAcademicDegreeByIndex(string id, int index)
        {
            var result = await employeeRepository.RemoveArrayElementByIndex<AcademicDegree>(id,
                                        index,
                                        employee => employee.AcademicDegrees,
                                        e => e.AcademicDegrees);
            return result.ModifiedCount;
        }

        // public async Task<EmployeesPaginationReedDto> GetEmployeesPagination(int page, int pageSize)
        // {
        //     var total = 
        //         await employeeRepository.TotalCountDocuments(FilterDefinition<Employee>.Empty);
        //
        //     var employees = 
        //         await employeeRepository.GetEmployeesByPage(FilterDefinition<Employee>.Empty, page, pageSize);
        //
        //     var employeesDto = mapper.Map<IEnumerable<EmployeeDto>>(employees);
        //     
        //     EmployeesPaginationReedDto response = new EmployeesPaginationReedDto
        //     {
        //         Page = page,
        //         PageSize = pageSize,
        //         TotalCount = total,
        //         Employees = employeesDto
        //     };
        //     return response;
        // }

        // public async Task<EmployeesPaginationReedDto> GetEmployeesPagination(int page, int pageSize)
        // {
        //     var total = 
        //         await employeeRepository.TotalCountDocuments(FilterDefinition<Employee>.Empty);
        //
        //     var employees = 
        //         await employeeRepository.GetEmployeesByPage2(FilterDefinition<Employee>.Empty, page, pageSize);
        //
        //     // var employeesDto = mapper.Map<IEnumerable<SimpleEmployeeDto>>(employees);
        //     
        //     EmployeesPaginationReedDto response = new EmployeesPaginationReedDto
        //     {
        //         Page = page,
        //         PageSize = pageSize,
        //         TotalCount = total,
        //         Employees = employees
        //     };
        //     return response;
        // }

        public async Task<EmployeesPaginationDto> GetEmployeesPagination(int page, int pageSize)
        {
            var total =
                await employeeRepository.TotalCountDocuments(FilterDefinition<Employee>.Empty);

            var projection = Builders<Employee>.Projection
                .Exclude(e => e.Id)
                .Include(e => e.FirstName)
                .Include(e => e.LastName)
                .Include(e => e.Patronymic)
                .Include(e => e.User.Email)
                .Include(e => e.User.Role);

            var bsonDocuments =
                await employeeRepository.GetEmployeesByPage(FilterDefinition<Employee>.Empty, projection, page, pageSize);

            var employees = mapper.Map<IEnumerable<SimpleEmployeeDto>>(bsonDocuments);

            EmployeesPaginationDto response = new()
            {
                Page = page,
                PageSize = pageSize,
                TotalCount = total,
                Employees = employees
            };
            return response;
        }
    }
}
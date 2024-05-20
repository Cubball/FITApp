using FITApp.EmployeesService.Dtos;

namespace FITApp.EmployeesService.Interfaces;

public interface IAdministrationService
{
    Task UpdateAsync(AdministrationDto administrationDto);

    Task<AdministrationDto> GetAsync();
}
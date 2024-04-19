using FITApp.IdentityService.Contracts.Requests;

namespace FITApp.IdentityService.Services;

public interface IEmployeeService
{
    Task<bool> CreateAsync(CreateEmployeeRequest user);

    Task<bool> UpdateAsync(UpdateEmployeeRequest user);

    Task<bool> DeleteAsync(string userId);
}
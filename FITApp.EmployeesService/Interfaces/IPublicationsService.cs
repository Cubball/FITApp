using FITApp.EmployeesService.Dtos;

namespace FITApp.EmployeesService.Interfaces;

public interface IPublicationsService
{
    Task<bool> UpdateAuthorDetailsAsync(string userId, AuthorDto employee);
    Task<bool> UpdateProfileDetailsAsync(AuthorDto author);
}
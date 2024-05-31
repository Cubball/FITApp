using FITApp.EmployeesService.Models;

namespace FITApp.EmployeesService.Interfaces
{
    public interface IAdministrationRepository
    {
        Task CreateAsync(Administration administration);

        Task UpdateAsync(Administration administration);

        Task<Administration> GetAsync();
    }
}
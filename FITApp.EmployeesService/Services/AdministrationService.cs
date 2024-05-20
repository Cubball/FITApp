using FITApp.EmployeesService.Dtos;
using FITApp.EmployeesService.Interfaces;
using FITApp.EmployeesService.Models;

namespace FITApp.EmployeesService.Services
{
    public class AdministrationService(IAdministrationRepository repository) : IAdministrationService
    {
        private readonly IAdministrationRepository _repository = repository;


        public async Task<AdministrationDto> GetAsync()
        {
            var model = await _repository.GetAsync();
            return Map(model);
        }

        public async Task UpdateAsync(AdministrationDto administrationDto)
        {
            var check = await _repository.GetAsync();

            if (check == null)
            {
                await _repository.CreateAsync(Map(administrationDto));
            }
            else
            {
                await _repository.UpdateAsync(Map(administrationDto));
            }
        }

        private static AdministrationDto Map(Administration administration)
        {
            return new AdministrationDto
            {
                HeadOfDepartment = new AuthorDto
                {
                    Id = administration.HeadOfDepartment.Id,
                    FirstName = administration.HeadOfDepartment.FirstName,
                    LastName = administration.HeadOfDepartment.LastName,
                    Patronymic = administration.HeadOfDepartment.Patronymic,
                },
                ScientificSecretary = new AuthorDto
                {
                    Id = administration.ScientificSecretary.Id,
                    FirstName = administration.ScientificSecretary.FirstName,
                    LastName = administration.ScientificSecretary.LastName,
                    Patronymic = administration.ScientificSecretary.Patronymic,
                },

            };
        }

        private static Administration Map(AdministrationDto administrationDto)
        {
            return new Administration
            {
                HeadOfDepartment = new Author
                {
                    Id = administrationDto.HeadOfDepartment.Id,
                    FirstName = administrationDto.HeadOfDepartment.FirstName,
                    LastName = administrationDto.HeadOfDepartment.LastName,
                    Patronymic = administrationDto.HeadOfDepartment.Patronymic,
                },
                ScientificSecretary = new Author
                {
                    Id = administrationDto.ScientificSecretary.Id,
                    FirstName = administrationDto.ScientificSecretary.FirstName,
                    LastName = administrationDto.ScientificSecretary.LastName,
                    Patronymic = administrationDto.ScientificSecretary.Patronymic,
                },
            };
        }
    }
}
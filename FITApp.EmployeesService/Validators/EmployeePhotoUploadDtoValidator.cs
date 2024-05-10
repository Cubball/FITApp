using FITApp.EmployeesService.Dtos;
using FluentValidation;

namespace FITApp.EmployeesService.Validators
{
    public class EmployeePhotoUploadDtoValidator : AbstractValidator<EmployeePhotoUploadDto>
    {
        public EmployeePhotoUploadDtoValidator()
        {
            RuleFor(x => x.File)
                .NotEmpty().WithMessage("Файл не може бути порожнім")
                .Must(BeAValidImage).WithMessage("Завантажений файл не є зображенням");
        }

        private bool BeAValidImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return false;

            var allowedMimeTypes = new[] { "image/jpeg", "image/png", "image/jpg" };

            return allowedMimeTypes.Contains(file.ContentType);
        }

    }
}

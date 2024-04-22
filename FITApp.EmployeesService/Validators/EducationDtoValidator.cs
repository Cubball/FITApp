using FITApp.EmployeesService.Dtos;
using FluentValidation;

namespace FITApp.EmployeesService.Validators
{
    public class EducationDtoValidator : AbstractValidator<EducationDto>
    {

        public EducationDtoValidator()
        {
            RuleFor(dto => dto.University)
                .NotEmpty().WithMessage("University name is required.")
                .Length(3, 250);

            RuleFor(dto => dto.Specialization)
                .NotEmpty().WithMessage("Specialization is required.")
                .Length(3, 250);

            RuleFor(dto => dto.DiplomaDateOfIssue)
                .NotEmpty().WithMessage("Diploma date of issue is required.")
                .SetValidator(new DateValidator<EducationDto>(DateTime.MinValue, DateTime.Now));
        }
    }
}
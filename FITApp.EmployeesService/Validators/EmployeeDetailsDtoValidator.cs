using FITApp.EmployeesService.Dtos;
using FluentValidation;

namespace FITApp.EmployeesService.Validators;

public class EmployeeDetailsDtoValidator : AbstractValidator<EmployeeDetailsDto>
{
    public EmployeeDetailsDtoValidator()
    {
        RuleFor(dto => dto.FirstName)
                    .NotEmpty().WithMessage("FirstName is required.")
                    .MaximumLength(50).WithMessage("FirstName must not exceed 50 characters.");

        RuleFor(dto => dto.LastName)
            .NotEmpty().WithMessage("LastName is required.")
            .MaximumLength(50).WithMessage("LastName must not exceed 50 characters.");

        RuleFor(dto => dto.Patronymic)
            .MaximumLength(50).WithMessage("Patronymic must not exceed 50 characters.");
        
        RuleFor(dto => dto.FirstNamePossessive)
            .NotEmpty().WithMessage("FirstName is required.")
            .MaximumLength(50).WithMessage("FirstName must not exceed 50 characters.");

        RuleFor(dto => dto.LastNamePossessive)
            .NotEmpty().WithMessage("LastName is required.")
            .MaximumLength(50).WithMessage("LastName must not exceed 50 characters.");

        RuleFor(dto => dto.PatronymicPossessive)
            .MaximumLength(50).WithMessage("Patronymic must not exceed 50 characters.");

        RuleFor(dto => dto.BirthDate)
            .NotNull().WithMessage("BirthDate is required.")
            .Must(BeValidDate).WithMessage("The value of {PropertyName} must be a valid date.")
            .SetValidator(new DateValidator<EmployeeDetailsDto>(DateTime.MinValue, DateTime.Now));
    }

    private bool BeValidDate(DateTime? date)
    {
        return date.HasValue && date.Value.Year >= 1900 && date.Value.Year <= DateTime.Now.Year;
    }
}
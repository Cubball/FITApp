using FITApp.EmployeesService.Dtos;
using FluentValidation;

namespace FITApp.EmployeesService.Validators;

public class AcademicDegreeDtoValidator : AbstractValidator<AcademicDegreeDto>
{
    public AcademicDegreeDtoValidator()
    {
        RuleFor(dto => dto.ShortName)
            .NotEmpty().WithMessage("Short name is required.")
            .Length(2, 50);

        RuleFor(dto => dto.FullName)
            .NotEmpty().WithMessage("Full name is required.")
            .Length(3, 250);
        RuleFor(dto => dto.DiplomaNumber).NotEmpty()
            .WithMessage("Diploma number is required.")
            .Length(3, 10);
        RuleFor(dto => dto.DateOfIssue)
            .NotEmpty().WithMessage("Date of issue is required.")
            .SetValidator(new DateValidator<AcademicDegreeDto>(DateTime.MinValue, DateTime.Now));
    }
}
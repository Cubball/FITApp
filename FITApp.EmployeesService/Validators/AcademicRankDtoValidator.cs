using FITApp.EmployeesService.Dtos;
using FluentValidation;

namespace FITApp.EmployeesService.Validators;

public class AcademicRankDtoValidator : AbstractValidator<AcademicRankDto>
{
    public AcademicRankDtoValidator()
    {
        RuleFor(dto => dto.Name)
            .NotEmpty().WithMessage("Name is required.")
            .Length(2, 50);

        RuleFor(dto => dto.CertificateNumber).NotEmpty()
            .WithMessage("Certificate number is required.")
            .Length(3, 10);
        RuleFor(dto => dto.DateOfIssue)
            .NotEmpty().WithMessage("Date of issue is required.")
            .SetValidator(new DateValidator<AcademicRankDto>(DateTime.MinValue, DateTime.Now));
    }
}
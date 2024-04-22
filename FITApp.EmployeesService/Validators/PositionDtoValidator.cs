using FITApp.EmployeesService.Dtos;
using FluentValidation;

namespace FITApp.EmployeesService.Validators
{
    public class PositionDtoValidator : AbstractValidator<PositionDto>
    {
        public PositionDtoValidator()
        {

            RuleFor(dto => dto.Name)
                .NotEmpty().WithMessage("Name is required.")
                .Length(3, 250);
            RuleFor(dto => dto.StartDate)
                .NotEmpty().WithMessage("Start date is required.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Start date must be in the past or today.");

            RuleFor(dto => dto.EndDate)
                .Must((dto, endDate) => endDate == null || endDate >= dto.StartDate)
                .WithMessage("End date must be after or equal to start date, or null.");
        }
    }

}
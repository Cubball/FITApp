using FluentValidation;
using FluentValidation.Validators;

namespace FITApp.EmployeesService.Validators;


public class DateValidator<T> : PropertyValidator<T, DateTime>
{
    private readonly DateTime _minDate;
    private readonly DateTime _maxDate;

    public DateValidator(DateTime minDate, DateTime maxDate)
    {
        _minDate = minDate;
        _maxDate = maxDate;
    }

    public override string Name => throw new NotImplementedException();

    public override bool IsValid(ValidationContext<T> context, DateTime value)
    {

        if (value < _minDate || value > _maxDate)
        {
            context.MessageFormatter.AppendArgument("MinDate", _minDate.ToShortDateString());
            context.MessageFormatter.AppendArgument("MaxDate", _maxDate.ToShortDateString());
            return false;
        }

        return true;
    }
}
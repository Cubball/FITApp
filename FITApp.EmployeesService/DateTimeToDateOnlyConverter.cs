using AutoMapper;

namespace FITApp.EmployeesService
{

    public class DateTimeToDateOnlyConverter : ITypeConverter<DateTime, DateOnly>
    {
        public DateOnly Convert(DateTime source, DateOnly destination, ResolutionContext context)
        {
            return new DateOnly(source.Year, source.Month, source.Day);
        }
    }
}
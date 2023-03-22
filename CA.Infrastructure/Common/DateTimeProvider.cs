using CA.Application.Common.Interfaces.Services;

namespace CA.Infrastructure.Common;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.Now;
}
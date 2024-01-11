using CA.Application.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace CA.Presentation.Filters.ExceptionFilter.ExceptionHandlers;

public class ForbiddenAccessExceptionHandler : ExceptionHandler<ForbiddenAccessException>
{
    protected override IActionResult HandleException(ForbiddenAccessException exception)
    {
        var details = new ProblemDetails
        {
            Status = StatusCodes.Status403Forbidden,
            Title = "Forbidden",
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.3"
        };

        return new ObjectResult(details)
        {
            StatusCode = StatusCodes.Status403Forbidden
        };
    }
}
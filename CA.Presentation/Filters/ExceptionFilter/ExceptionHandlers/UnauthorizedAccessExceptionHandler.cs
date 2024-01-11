using Microsoft.AspNetCore.Mvc;

namespace CA.Presentation.Filters.ExceptionFilter.ExceptionHandlers;

public class UnauthorizedAccessExceptionHandler : ExceptionHandler<UnauthorizedAccessException>
{
    protected override IActionResult HandleException(UnauthorizedAccessException exception)
    {
        var details = new ProblemDetails
        {
            Status = StatusCodes.Status401Unauthorized,
            Title = "Unauthorized",
            Type = "https://tools.ietf.org/html/rfc7235#section-3.1"
        };

        return new ObjectResult(details)
        {
            StatusCode = StatusCodes.Status401Unauthorized
        };
    }
}
using Microsoft.AspNetCore.Mvc;

namespace CA.Presentation.Middlewares.GlobalExceptionHandler.Handlers;

public class UnauthorizedAccessExceptionHandler : ExceptionHandler<UnauthorizedAccessException>
{
    protected override ProblemDetails HandleException(UnauthorizedAccessException exception)
    {
        return new ProblemDetails
        {
            Status = StatusCodes.Status401Unauthorized,
            Title = "Unauthorized",
            Type = "https://tools.ietf.org/html/rfc7235#section-3.1"
        };
    }
}
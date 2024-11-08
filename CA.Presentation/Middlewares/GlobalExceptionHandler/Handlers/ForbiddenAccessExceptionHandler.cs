using CA.Application.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace CA.Presentation.Middlewares.GlobalExceptionHandler.Handlers;

public class ForbiddenAccessExceptionHandler : ExceptionHandler<ForbiddenAccessException>
{
    protected override ProblemDetails HandleException(ForbiddenAccessException exception)
    {
        return new ProblemDetails
        {
            Status = StatusCodes.Status403Forbidden,
            Title = "Forbidden",
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.3"
        };
    }
}
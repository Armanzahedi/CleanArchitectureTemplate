using CA.Application.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace CA.Presentation.Middlewares.GlobalExceptionHandler.Handlers;

public class ValidationExceptionHandler : ExceptionHandler<ValidationException>
{
    protected override ProblemDetails HandleException(ValidationException exception)
    {
        return new ValidationProblemDetails(exception.Errors)
        {
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            Status = StatusCodes.Status400BadRequest
        };
    }
}
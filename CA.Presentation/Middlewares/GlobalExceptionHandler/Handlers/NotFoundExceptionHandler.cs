using Microsoft.AspNetCore.Mvc;

namespace CA.Presentation.Middlewares.GlobalExceptionHandler.Handlers;

public class NotFoundExceptionHandler : ExceptionHandler<Application.Common.Exceptions.NotFoundException>
{
    protected override ProblemDetails HandleException(Application.Common.Exceptions.NotFoundException exception)
    {
        return new ProblemDetails()
        {
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
            Title = "The specified resource was not found.",
            Detail = exception.Message,
            Status = StatusCodes.Status404NotFound
        };
    }
}
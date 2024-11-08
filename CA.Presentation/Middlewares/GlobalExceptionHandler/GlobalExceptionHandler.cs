using Microsoft.AspNetCore.Mvc;

namespace CA.Presentation.Middlewares.GlobalExceptionHandler;

public class GlobalExceptionHandler(IProblemDetailsService problemDetailsService) : Microsoft.AspNetCore.Diagnostics.IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
        CancellationToken cancellationToken)
    {
        var handler = ExceptionHandlerProvider.GetHandler(exception.GetType());

        if (handler == null)
        {
            return await problemDetailsService.TryWriteAsync(new ProblemDetailsContext
            {
                HttpContext = httpContext,
                Exception = exception,
                ProblemDetails = new ProblemDetails()
                {
                    Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1",
                    Title = "An error occurred while processing your request.",
                    Detail = exception.Message,
                    Status = StatusCodes.Status500InternalServerError
                }
            });
        }

        var problemDetailsContext = handler.Invoke(httpContext, exception);

        httpContext.Response.StatusCode =
            problemDetailsContext.ProblemDetails.Status ?? StatusCodes.Status500InternalServerError;
        return await problemDetailsService.TryWriteAsync(problemDetailsContext);
    }
}
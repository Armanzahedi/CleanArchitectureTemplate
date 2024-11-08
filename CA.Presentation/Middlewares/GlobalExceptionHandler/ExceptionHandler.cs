using Microsoft.AspNetCore.Mvc;

namespace CA.Presentation.Middlewares.GlobalExceptionHandler;

public interface IExceptionHandler
{
    ProblemDetailsContext Invoke(HttpContext httpContext,Exception exception);
}
public abstract class ExceptionHandler<T1> : IExceptionHandler where T1 : Exception
{
    public ProblemDetailsContext Invoke(HttpContext httpContext,Exception exception)
    {
        var problemDetails = HandleException((T1)exception);
        return new ProblemDetailsContext
        {
            HttpContext = httpContext,
            ProblemDetails = problemDetails,
            Exception = exception
        };
    }

    protected abstract ProblemDetails HandleException(T1 exception);
}


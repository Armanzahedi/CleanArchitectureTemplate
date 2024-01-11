using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CA.Presentation.Filters.ExceptionFilter;

public interface IExceptionHandler
{
    void Invoke(ExceptionContext context);
}
public abstract class ExceptionHandler<T1> : IExceptionHandler where T1 : Exception
{
    public void Invoke(ExceptionContext context)
    {
        var exception = (T1)context.Exception;
        context.Result = HandleException(exception);
        context.ExceptionHandled = true;
    }

    protected abstract IActionResult HandleException(T1 exception);
}


using CA.Application.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CA.Presentation.Filters.ExceptionFilter;

public class ExceptionFilterAttribute : Microsoft.AspNetCore.Mvc.Filters.ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        HandleException(context);
        base.OnException(context);
    }

    private void HandleException(ExceptionContext context)
    {
        var handler = ExceptionHandlerProvider.GetHandler(context.Exception.GetType());
        
        if(handler != null)
            handler.Invoke(context);
        
        else if (!context.ModelState.IsValid)
        {
            HandleInvalidModelStateException(context);
        }
    }

    private void HandleInvalidModelStateException(ExceptionContext context)
    {
        var details = new ValidationProblemDetails(context.ModelState)
        {
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
        };

        context.Result = new BadRequestObjectResult(details);

        context.ExceptionHandled = true;
    }

}
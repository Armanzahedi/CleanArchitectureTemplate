using CA.Presentation.Filters;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CA.Presentation.Controllers.Api.V1._0;

[ApiController]
[ApiExceptionFilter]
[Route("api/V1.0/[controller]")]
public abstract class ApiControllerBase : ControllerBase
{
    private ISender? _mediator;
    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
}
using Microsoft.AspNetCore.Mvc;

namespace CA.Presentation.Controllers;
public class ProjectsController : BaseController
{
    public IActionResult Test()
    {
        return View();
    }
}
using CA.Presentation.Models.Projects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using CA.Application.Common.Models;
using CA.Application.Projects;
using CA.Application.Projects.Commands.AddTodo;
using CA.Application.Projects.Commands.CreateProject;
using CA.Application.Projects.Commands.DeleteProject;
using CA.Application.Projects.Commands.UpdateProject;
using CA.Application.Projects.Queries.GetProject;
using CA.Application.Projects.Queries.GetProjects;

namespace CA.Presentation.Controllers.Api.V1._0;

public class ProjectsController : ApiControllerBase
{
    [HttpPost]
    public async Task<ActionResult> AddProject([FromBody] CreateProjectCommand command)
    {
        var result = await Mediator.Send(command);
        return CreatedAtAction("GetProject", new { projectId = result.Id },result);
    }
    
    [HttpGet("{projectId}")]
    public async Task<ActionResult<ProjectResult>> GetProject(GetProjectQuery query)
    {
        return await Mediator.Send(query);
    }
    
    [HttpGet]
    public async Task<ActionResult<PaginatedList<ProjectResult>>> GetProjects([FromQuery] GetProjectsQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpPut]
    public async Task<ActionResult> UpdateProject(Guid projectId, [FromBody] UpdateProjectRequest request)
    {
        await Mediator.Send(new UpdateProjectCommand(projectId,request.description));
        return NoContent();
    }
    
    [HttpDelete]
    public async Task<ActionResult> DeleteProject(Guid projectId)
    {
        await Mediator.Send(new DeleteProjectCommand(projectId));
        return NoContent();
    }
    
    [HttpPost("{projectId}/todos")]
    public async Task<ActionResult> AddTodo(Guid projectId, [FromBody] AddTodoRequest request)
    {
        await Mediator.Send(new AddTodoCommand(projectId,request.title));
        return NoContent();
    }
}
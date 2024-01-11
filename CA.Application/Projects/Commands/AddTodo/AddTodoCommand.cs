using CA.Application.Common.Interfaces.Persistence;
using MediatR;
using CA.Domain.Project;

namespace CA.Application.Projects.Commands.AddTodo;

public record AddTodoCommand(Guid projectId,string todoTile) : IRequest
{
}

public class AddTodoCommandHandler : IRequestHandler<AddTodoCommand>
{
    private readonly IRepository<Project> _projectRepository;

    public AddTodoCommandHandler(IRepository<Project> projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task Handle(AddTodoCommand request, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetByIdAsync(request.projectId);
        project.AddTodo(request.todoTile,null);
        await _projectRepository.UpdateAsync(project);
    }
}
using CA.Application.Common.Exceptions;
using CA.Application.Common.Interfaces.Persistence;
using MediatR;
using CA.Domain.Project;

namespace CA.Application.Projects.Commands.DeleteProject;

public record DeleteProjectCommand(Guid projectId) : IRequest;

public class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand>
{
    private readonly IRepository<Project> _projectRepository;

    public DeleteProjectCommandHandler(IRepository<Project> projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetByIdAsync(request.projectId, cancellationToken);
        if (project == null) throw new NotFoundException();
        
        await _projectRepository.DeleteAsync(project, cancellationToken);
    }
}
using CA.Application.Common.Interfaces.Persistence;
using MapsterMapper;
using MediatR;
using CA.Domain.Project;

namespace CA.Application.Projects.Commands.CreateProject;

public record CreateProjectCommand(string title,string description) : IRequest<ProjectResult> { }

public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, ProjectResult>
{
    private readonly IRepository<Project> _projectRepository;
    private readonly IMapper _mapper;

    public CreateProjectCommandHandler(IRepository<Project> projectRepository, IMapper mapper)
    {
        _projectRepository = projectRepository;
        _mapper = mapper;
    }

    public async Task<ProjectResult> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        var project = new Project(request.title,request.description);
        await _projectRepository.AddAsync(project, cancellationToken);
        
        return _mapper.Map<ProjectResult>(project);
    }
}
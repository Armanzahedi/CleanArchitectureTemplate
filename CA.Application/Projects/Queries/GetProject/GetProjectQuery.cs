using CA.Application.Common.Interfaces.Persistence;
using CA.Application.Projects.Specifications;
using MapsterMapper;
using MediatR;
using CA.Domain.Project;

namespace CA.Application.Projects.Queries.GetProject;

public record GetProjectQuery(Guid projectId) : IRequest<ProjectResult>;

public class GetProjectsQueryHandler : IRequestHandler<GetProjectQuery,ProjectResult>
{
    private readonly IReadRepository<Project> _projectRepository;
    private readonly IMapper _mapper;

    public GetProjectsQueryHandler(IReadRepository<Project> projectRepository,
        IMapper mapper)
    {
        _projectRepository = projectRepository;
        _mapper = mapper;
    }

    public async Task<ProjectResult> Handle(GetProjectQuery request, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.FirstOrDefaultAsync(new ProjectByIdWithItemsSpec(request.projectId), cancellationToken);
        return _mapper.Map<ProjectResult>(project);
    }
}

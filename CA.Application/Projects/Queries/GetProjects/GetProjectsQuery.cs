using CA.Application.Common.Exceptions;
using CA.Application.Common.Extensions.PaginatedList;
using CA.Application.Common.Interfaces.Persistence;
using MapsterMapper;
using MediatR;
using CA.Application.Projects.Queries.GetProject;
using CA.Domain.Project;
using CA.Domain.Project.Specs;

namespace CA.Application.Projects.Queries.GetProjects;

public record GetProjectsQuery(int? pageNumber, int? pageSize) : IRequest<PaginatedList<ProjectResult>>;

public class GetProjectsQueryHandler : IRequestHandler<GetProjectsQuery, PaginatedList<ProjectResult>>
{
    private readonly IReadRepository<Project> _projectRepository;
    private readonly IMapper _mapper;

    public GetProjectsQueryHandler(IReadRepository<Project> projectRepository, IMapper mapper)
    {
        _projectRepository = projectRepository;
        _mapper = mapper;
    }

    public async Task<PaginatedList<ProjectResult>> Handle(GetProjectsQuery request,
        CancellationToken cancellationToken)
    {
        var result =
            await _projectRepository
                .PaginatedListAsync<Project, ProjectResult>(new ProjectWithItemsSpec(),
                    request.pageNumber,
                    request.pageSize,
                    cancellationToken);
        return result;
    }
}
using Ardalis.Specification;
using CA.Domain.Project;

namespace CA.Application.Projects.Specifications;

public sealed class ProjectByIdWithItemsSpec : Specification<Project>
{
    public ProjectByIdWithItemsSpec(Guid projectId)
    {
        Query.Where(p => p.Id == projectId)
            .Include(p => p.Items);
    }
}
using Ardalis.Specification;

namespace CA.Domain.Project.Specs;

public sealed class ProjectByIdWithItemsSpec : Specification<Project>
{
    public ProjectByIdWithItemsSpec(Guid projectId)
    {
        Query.Where(p => p.Id == projectId)
            .Include(p => p.Items);
    }
}
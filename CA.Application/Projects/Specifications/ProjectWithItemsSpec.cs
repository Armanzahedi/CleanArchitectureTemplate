using Ardalis.Specification;
using CA.Domain.Project;

namespace CA.Application.Projects.Specifications;

public class ProjectWithItemsSpec : Specification<Project>
{
    public ProjectWithItemsSpec()
    {
        Query.Include(x => x.Items);
    }
}
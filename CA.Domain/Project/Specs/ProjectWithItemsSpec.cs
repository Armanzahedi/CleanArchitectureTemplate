using Ardalis.Specification;

namespace CA.Domain.Project.Specs;

public class ProjectWithItemsSpec : Specification<Project>
{
    public ProjectWithItemsSpec()
    {
        Query.Include(x => x.Items);
    }
}
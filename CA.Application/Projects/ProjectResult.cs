namespace CA.Application.Projects;

public class ProjectResult
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public List<string> Todos { get; set; }
}
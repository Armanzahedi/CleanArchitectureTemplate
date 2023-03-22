using Mapster;
using CA.Application.Projects.Queries.GetProject;
using CA.Domain.Project;

namespace CA.Application.Projects;

public class ProjectMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Project, ProjectResult>()
            .Map(dest => dest.Title, src => src.Description)
            .Map(dest => dest.Todos, src => src.Items.Select(i => i.Title));
    }
}
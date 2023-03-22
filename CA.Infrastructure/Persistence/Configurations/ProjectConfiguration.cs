using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CA.Domain.Project;

namespace CA.Infrastructure.Persistence.Configurations;

public class ProjectConfiguration: BaseEntityTypeConfiguration<Project>
{
    public override void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.Property(t => t.Description)
            .HasMaxLength(200)
            .IsRequired();
        base.Configure(builder);
    }
}
namespace CA.Domain.Common.Entity;

public interface ISoftDeletableEntity
{
    public bool IsDeleted { get; set; }
}
namespace CA.Domain.Common.Interfaces;

public interface ISoftDeletableEntity
{
    public bool IsDeleted { get; set; }
}
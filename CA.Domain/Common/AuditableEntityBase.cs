using CA.Domain.Common.Attributes;
using CA.Domain.Common.Interfaces;

namespace CA.Domain.Common;

[Auditable]
public class AuditableEntityBase<TId>: EntityBase<TId>
{
    public Guid? CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public Guid? LastModifiedBy { get; set; }
    public DateTime? LastModifiedOn { get; set; }
}   
public class AuditableEntityBase: AuditableEntityBase<int>
{
}   
namespace CA.Application.Common.Specifications;

public class FilterBase
{
    public bool LoadChildren { get; set; }
    public bool IsPagingEnabled { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
}
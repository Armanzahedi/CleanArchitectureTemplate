namespace CA.Application.Common.Extensions.PaginatedList;

public class PaginatedList<T>
{
    public PaginatedList(IReadOnlyCollection<T> items, int totalCount, int? pageNumber, int? pageSize)
    {
        PageSize = pageSize;
        PageNumber = pageNumber;
        TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
        TotalCount = totalCount;
        Items = items;
    }
    public IReadOnlyCollection<T> Items { get; }
    public int? PageNumber { get; } = 1;
    public int TotalPages { get; }
    public int TotalCount { get; }

    public int? PageSize { get; set; } = 10;


    public bool HasPreviousPage => PageNumber > 1;

    public bool HasNextPage => PageNumber < TotalPages;
}
using Ardalis.Specification;
using Mapster;
using CA.Application.Common.Specifications;

namespace CA.Application.Common.Models;

public static class PaginatedListExtensions
{
    public static async Task<PaginatedList<T>> PaginatedListAsync<T>(
        this IReadRepositoryBase<T> repository,
        ISpecification<T> spec,
        int? pageNumber,
        int? pageSize,
        CancellationToken cancellationToken = default)
        where T : class
    {
        pageNumber ??= 1;
        pageSize ??= 10;
        spec.Query.Paginate(pageNumber.Value, pageSize.Value);
        var list = await repository.ListAsync(spec, cancellationToken);
        var count = await repository.CountAsync(spec, cancellationToken);
        return new PaginatedList<T>(list, count, pageNumber, pageSize);
    }
    public static async Task<PaginatedList<TMapped>> PaginatedListAsync<T, TMapped>(
        this IReadRepositoryBase<T> repository,
        ISpecification<T> spec,
        int? pageNumber = 1,
        int? pageSize = 10,
        CancellationToken cancellationToken = default)
        where T : class
        where TMapped : class
    {
        pageNumber ??= 1;
        pageSize ??= 10;
        spec.Query.Paginate(pageNumber.Value, pageSize.Value);
        var list = await repository.ListAsync(spec, cancellationToken);
        var count = await repository.CountAsync(spec, cancellationToken);
        return new PaginatedList<TMapped>(list.Adapt<List<TMapped>>(), count, pageNumber, pageSize);
    }

}
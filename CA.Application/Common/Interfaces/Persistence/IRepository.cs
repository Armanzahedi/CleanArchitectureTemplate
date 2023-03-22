using Ardalis.Specification;
using CA.Application.Common.Models;
using CA.Domain.Common.Interfaces;

namespace CA.Application.Common.Interfaces.Persistence;

public interface IRepository<T> : IRepositoryBase<T> where T : class, IAggregateRoot
{
}

public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class, IAggregateRoot
{
    // Task<PaginatedList<T>> PaginatedListAsync(int page = 1,
    //     int pageSize = 10,
    //     CancellationToken cancellationToken = default);
    //
    // Task<PaginatedList<T>> PaginatedListAsync(ISpecification<T> specification,
    //     int page = 1,
    //     int pageSize = 10,
    //     CancellationToken cancellationToken = default);
    // Task<PaginatedList<TResult>> PaginatedListAsync<TResult>(ISpecification<T, TResult> specification,
    //     int page = 1,
    //     int pageSize = 10,
    //     CancellationToken cancellationToken = default);
    
    
}
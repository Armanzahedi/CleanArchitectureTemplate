using Ardalis.Specification;
using CA.Application.Common.Extensions.PaginatedList;
using CA.Application.Common.Interfaces.Persistence;
using CA.Domain.Common.Entity;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace CA.Infrastructure.Persistence.Repository;

public class CachedRepository<T>(IMemoryCache cache,
    ILogger<CachedRepository<T>> logger,
    EfRepository<T> sourceRepository)
  : IReadRepository<T>
  where T : class, IAggregateRoot
{
  private MemoryCacheEntryOptions _cacheOptions = new MemoryCacheEntryOptions()
    .SetAbsoluteExpiration(relative: TimeSpan.FromSeconds(10));

  /// <inheritdoc/>
  public Task<bool> AnyAsync(ISpecification<T> specification, CancellationToken cancellationToken = new CancellationToken())
  {
    // TODO: Add Caching
    return sourceRepository.AnyAsync(specification, cancellationToken);
  }

  /// <inheritdoc/>
  public Task<bool> AnyAsync(CancellationToken cancellationToken = default)
  {
    // TODO: Add Caching
    return sourceRepository.AnyAsync(cancellationToken);
  }

  public IAsyncEnumerable<T> AsAsyncEnumerable(ISpecification<T> specification)
  {
    return sourceRepository.AsAsyncEnumerable(specification);
  }

  /// <inheritdoc/>
  public Task<int> CountAsync(ISpecification<T> specification, CancellationToken cancellationToken = new CancellationToken())
  {
    // TODO: Add Caching
    return sourceRepository.CountAsync(specification, cancellationToken);
  }

  /// <inheritdoc/>
  public Task<int> CountAsync(CancellationToken cancellationToken = default)
  {
    // TODO: Add Caching
    return sourceRepository.CountAsync(cancellationToken);
  }

  /// <inheritdoc/>
  public Task<T> GetByIdAsync(int id, CancellationToken cancellationToken = default)
  {
    // TODO: Add Caching
    return sourceRepository.GetByIdAsync(id, cancellationToken);
  }

  /// <inheritdoc/>
  public Task<T> GetByIdAsync<TId>(TId id, CancellationToken cancellationToken = default)
  {
    // TODO: Add Caching
    return sourceRepository.GetByIdAsync(id, cancellationToken);
  }

  /// <inheritdoc/>
  public Task<T> GetBySpecAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
  {
    if (specification.CacheEnabled)
    {
      string key = $"{specification.CacheKey}-GetBySpecAsync";
      logger.LogInformation("Checking cache for " + key);
      return cache.GetOrCreate(key, entry =>
      {
        entry.SetOptions(_cacheOptions);
        logger.LogWarning("Fetching source data for " + key);
        return sourceRepository.GetBySpecAsync(specification, cancellationToken);
      });
    }
    return sourceRepository.GetBySpecAsync(specification, cancellationToken);
  }
  
  /// <inheritdoc/>
  public Task<TResult?> GetBySpecAsync<TResult>(ISpecification<T, TResult> specification,
    CancellationToken cancellationToken = new CancellationToken())
  {
    if (specification.CacheEnabled)
    {
      string key = $"{specification.CacheKey}-GetBySpecAsync";
      logger.LogInformation("Checking cache for " + key);
      return cache.GetOrCreate(key, entry =>
      {
        entry.SetOptions(_cacheOptions);
        logger.LogWarning("Fetching source data for " + key);
        return sourceRepository.GetBySpecAsync(specification, cancellationToken);
      });
    }
    return sourceRepository.GetBySpecAsync<TResult>(specification, cancellationToken);
  }

  /// <inheritdoc/>
  public virtual async Task<T?> FirstOrDefaultAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
  {
    return await sourceRepository.FirstOrDefaultAsync(specification, cancellationToken);
  }

  /// <inheritdoc/>
  public virtual async Task<TResult?> FirstOrDefaultAsync<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default)
  {
    return await sourceRepository.FirstOrDefaultAsync(specification, cancellationToken);
  }

  /// <inheritdoc/>
  public virtual async Task<T?> SingleOrDefaultAsync(ISingleResultSpecification<T> specification, CancellationToken cancellationToken = default)
  {
    return await sourceRepository.SingleOrDefaultAsync(specification, cancellationToken);
  }

  /// <inheritdoc/>
  public virtual async Task<TResult?> SingleOrDefaultAsync<TResult>(ISingleResultSpecification<T, TResult> specification, CancellationToken cancellationToken = default)
  {
    return await sourceRepository.SingleOrDefaultAsync(specification, cancellationToken);
  }

  /// <inheritdoc/>
  public Task<List<T>> ListAsync(CancellationToken cancellationToken = default)
  {
    string key = $"{nameof(T)}-ListAsync";
    return cache.GetOrCreate(key, entry =>
    {
      entry.SetOptions(_cacheOptions);
      return sourceRepository.ListAsync(cancellationToken);
    });
  }

  public Task<List<T>> ListAsync(ISpecification<T> specification, CancellationToken cancellationToken = new CancellationToken())
  {
    if (specification.CacheEnabled)
    {
      string key = $"{specification.CacheKey}-ListAsync";
      logger.LogInformation("Checking cache for " + key);
      return cache.GetOrCreate(key, entry =>
      {
        entry.SetOptions(_cacheOptions);
        logger.LogWarning("Fetching source data for " + key);
        return sourceRepository.ListAsync(specification, cancellationToken);
      });
    }
    return sourceRepository.ListAsync(specification, cancellationToken);
  }

  public Task<List<TResult>> ListAsync<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = new CancellationToken())
  {
    if (specification.CacheEnabled)
    {
      string key = $"{specification.CacheKey}-ListAsync";
      logger.LogInformation("Checking cache for " + key);
      return cache.GetOrCreate(key, entry =>
      {
        entry.SetOptions(_cacheOptions);
        logger.LogWarning("Fetching source data for " + key);
        return sourceRepository.ListAsync(specification, cancellationToken);
      });
    }
    return sourceRepository.ListAsync(specification, cancellationToken);
  }

}
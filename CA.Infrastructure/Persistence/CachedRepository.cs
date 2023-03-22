using Ardalis.Specification;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using CA.Application.Common.Interfaces.Persistence;
using CA.Application.Common.Models;
using CA.Domain.Common.Interfaces;

namespace CA.Infrastructure.Persistence;

public class CachedRepository<T> : IReadRepository<T> where T : class, IAggregateRoot
{
  private readonly IMemoryCache _cache;
  private readonly ILogger<CachedRepository<T>> _logger;
  private readonly EfRepository<T> _sourceRepository;
  private MemoryCacheEntryOptions _cacheOptions;

  public CachedRepository(IMemoryCache cache,
      ILogger<CachedRepository<T>> logger,
      EfRepository<T> sourceRepository)
  {
    _cache = cache;
    _logger = logger;
    _sourceRepository = sourceRepository;

    _cacheOptions = new MemoryCacheEntryOptions()
        .SetAbsoluteExpiration(relative: TimeSpan.FromSeconds(10));
  }

  /// <inheritdoc/>
  public Task<bool> AnyAsync(ISpecification<T> specification, CancellationToken cancellationToken = new CancellationToken())
  {
    // TODO: Add Caching
    return _sourceRepository.AnyAsync(specification, cancellationToken);
  }

  /// <inheritdoc/>
  public Task<bool> AnyAsync(CancellationToken cancellationToken = default)
  {
    // TODO: Add Caching
    return _sourceRepository.AnyAsync(cancellationToken);
  }

  /// <inheritdoc/>
  public Task<int> CountAsync(ISpecification<T> specification, CancellationToken cancellationToken = new CancellationToken())
  {
    // TODO: Add Caching
    return _sourceRepository.CountAsync(specification, cancellationToken);
  }

  /// <inheritdoc/>
  public Task<int> CountAsync(CancellationToken cancellationToken = default)
  {
    // TODO: Add Caching
    return _sourceRepository.CountAsync(cancellationToken);
  }

  /// <inheritdoc/>
  public Task<T> GetByIdAsync(int id, CancellationToken cancellationToken = default)
  {
    // TODO: Add Caching
    return _sourceRepository.GetByIdAsync(id, cancellationToken);
  }

  /// <inheritdoc/>
  public Task<T> GetByIdAsync<TId>(TId id, CancellationToken cancellationToken = default)
  {
    // TODO: Add Caching
    return _sourceRepository.GetByIdAsync(id, cancellationToken);
  }

  /// <inheritdoc/>
  public Task<T> GetBySpecAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
  {
    if (specification.CacheEnabled)
    {
      string key = $"{specification.CacheKey}-GetBySpecAsync";
      _logger.LogInformation("Checking cache for " + key);
      return _cache.GetOrCreate(key, entry =>
      {
        entry.SetOptions(_cacheOptions);
        _logger.LogWarning("Fetching source data for " + key);
        return _sourceRepository.GetBySpecAsync(specification, cancellationToken);
      });
    }
    return _sourceRepository.GetBySpecAsync(specification, cancellationToken);
  }
  
  /// <inheritdoc/>
  public Task<TResult?> GetBySpecAsync<TResult>(ISpecification<T, TResult> specification,
    CancellationToken cancellationToken = new CancellationToken())
  {
    if (specification.CacheEnabled)
    {
      string key = $"{specification.CacheKey}-GetBySpecAsync";
      _logger.LogInformation("Checking cache for " + key);
      return _cache.GetOrCreate(key, entry =>
      {
        entry.SetOptions(_cacheOptions);
        _logger.LogWarning("Fetching source data for " + key);
        return _sourceRepository.GetBySpecAsync(specification, cancellationToken);
      });
    }
    return _sourceRepository.GetBySpecAsync<TResult>(specification, cancellationToken);
  }

  /// <inheritdoc/>
  public virtual async Task<T?> FirstOrDefaultAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
  {
    return await _sourceRepository.FirstOrDefaultAsync(specification, cancellationToken);
  }

  /// <inheritdoc/>
  public virtual async Task<TResult?> FirstOrDefaultAsync<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default)
  {
    return await _sourceRepository.FirstOrDefaultAsync(specification, cancellationToken);
  }

  /// <inheritdoc/>
  public virtual async Task<T?> SingleOrDefaultAsync(ISingleResultSpecification<T> specification, CancellationToken cancellationToken = default)
  {
    return await _sourceRepository.SingleOrDefaultAsync(specification, cancellationToken);
  }

  /// <inheritdoc/>
  public virtual async Task<TResult?> SingleOrDefaultAsync<TResult>(ISingleResultSpecification<T, TResult> specification, CancellationToken cancellationToken = default)
  {
    return await _sourceRepository.SingleOrDefaultAsync(specification, cancellationToken);
  }

  /// <inheritdoc/>
  public Task<List<T>> ListAsync(CancellationToken cancellationToken = default)
  {
    string key = $"{nameof(T)}-ListAsync";
    return _cache.GetOrCreate(key, entry =>
    {
      entry.SetOptions(_cacheOptions);
      return _sourceRepository.ListAsync(cancellationToken);
    });
  }

  public Task<List<T>> ListAsync(ISpecification<T> specification, CancellationToken cancellationToken = new CancellationToken())
  {
    if (specification.CacheEnabled)
    {
      string key = $"{specification.CacheKey}-ListAsync";
      _logger.LogInformation("Checking cache for " + key);
      return _cache.GetOrCreate(key, entry =>
      {
        entry.SetOptions(_cacheOptions);
        _logger.LogWarning("Fetching source data for " + key);
        return _sourceRepository.ListAsync(specification, cancellationToken);
      });
    }
    return _sourceRepository.ListAsync(specification, cancellationToken);
  }

  public Task<List<TResult>> ListAsync<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = new CancellationToken())
  {
    if (specification.CacheEnabled)
    {
      string key = $"{specification.CacheKey}-ListAsync";
      _logger.LogInformation("Checking cache for " + key);
      return _cache.GetOrCreate(key, entry =>
      {
        entry.SetOptions(_cacheOptions);
        _logger.LogWarning("Fetching source data for " + key);
        return _sourceRepository.ListAsync(specification, cancellationToken);
      });
    }
    return _sourceRepository.ListAsync(specification, cancellationToken);
  }

  public async Task<PaginatedList<T>> PaginatedListAsync(int page = 1, int pageSize = 10, CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public async Task<PaginatedList<T>> PaginatedListAsync(ISpecification<T> specification, int page = 1, int pageSize = 10,
    CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }

  public async Task<PaginatedList<TResult>> PaginatedListAsync<TResult>(ISpecification<T, TResult> specification, int page = 1, int pageSize = 10,
    CancellationToken cancellationToken = default)
  {
    throw new NotImplementedException();
  }
}
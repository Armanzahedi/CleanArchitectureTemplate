using Ardalis.Specification.EntityFrameworkCore;
using CA.Application.Common.Interfaces.Persistence;
using CA.Domain.Common.Entity;

namespace CA.Infrastructure.Persistence.Repository;

public class EfRepository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T> where T : class, IAggregateRoot
{
    private readonly AppDbContext _dbContext;
    public EfRepository(AppDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}
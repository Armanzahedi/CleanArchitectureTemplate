using CA.Application.Common.Interfaces.Services;

namespace CA.Infrastructure.Identity;

public class CurrentUserService : ICurrentUserService
{
    public Guid? UserId { get; }
}
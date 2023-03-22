namespace CA.Application.Common.Interfaces.Services;

public interface ICurrentUserService
{
    Guid? UserId { get; }
}
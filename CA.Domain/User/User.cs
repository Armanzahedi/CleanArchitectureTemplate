using CA.Domain.Common.Entity;
using Microsoft.AspNetCore.Identity;

namespace CA.Domain.User;

public class User : IdentityUser, IAggregateRoot
{
    
}
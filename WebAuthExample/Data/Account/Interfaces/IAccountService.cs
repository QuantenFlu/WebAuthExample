using Microsoft.AspNetCore.Identity;

namespace WebAuthExample.Data.Account;

public interface IAccountService
{
    Task<IdentityResult> CreateUserAsync(CustomUser user, CancellationToken cancellationToken);
    Task<IdentityResult> DeleteUserAsync(CustomUser user, CancellationToken cancellationToken);
    Task<IdentityResult> UpdateUserAsync(CustomUser user, CancellationToken cancellationToken);
    Task<CustomUser?> GetUserByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<CustomUser?> GetUserByNameAsync(string username, CancellationToken cancellationToken);
    Task<string?> GetPasswordHashAsync(CustomUser user, CancellationToken cancellationToken);
    Task SetPasswordHashAsync(CustomUser user, string? passwordHash, CancellationToken cancellationToken);
    Task<bool> HasPasswordAsync(CustomUser user, CancellationToken cancellationToken);
    Task<string?> GetUserIdAsync(CustomUser user, CancellationToken cancellationToken);
    Task<string?> GetUserNameAsync(CustomUser user, CancellationToken cancellationToken);
    Task SetUserNameAsync(CustomUser user, string? userName, CancellationToken cancellationToken);
}
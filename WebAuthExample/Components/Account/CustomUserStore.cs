using Microsoft.AspNetCore.Identity;
using WebAuthExample.Data.Account;

namespace WebAuthExample.Components.Account;

public class CustomUserStore : IUserStore<CustomUser> , IUserPasswordStore<CustomUser>
{
    private readonly IAccountService _accountService;

    public CustomUserStore(IAccountService accountService)
    {
        _accountService = accountService;
    }

    // IUserStore methods
    public Task<IdentityResult> CreateAsync(CustomUser user, CancellationToken cancellationToken)
    {
        _accountService.CreateUserAsync(user, cancellationToken);
        return Task.FromResult(IdentityResult.Success);
    }

    public Task<IdentityResult> DeleteAsync(CustomUser user, CancellationToken cancellationToken)
    {
        _accountService.DeleteUserAsync(user, cancellationToken);
        return Task.FromResult(IdentityResult.Success);
    }

    public async Task<CustomUser?> FindByIdAsync(string userId, CancellationToken cancellationToken)
    {
        var id = Guid.Parse(userId);
        return await _accountService.GetUserByIdAsync(id, cancellationToken);
    }

    public async Task<CustomUser?> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
    {
        return await _accountService.GetUserByNameAsync(normalizedUserName, cancellationToken);
    }

    public Task<string?> GetNormalizedUserNameAsync(CustomUser user, CancellationToken cancellationToken)
    {
        return Task.FromResult(user.UserName.ToUpper());
    }

    public Task<string?> GetUserIdAsync(CustomUser user, CancellationToken cancellationToken)
    {
        return _accountService.GetUserIdAsync(user, cancellationToken);
    }

    public Task<string?> GetUserNameAsync(CustomUser user, CancellationToken cancellationToken)
    {
        return _accountService.GetUserNameAsync(user, cancellationToken);
    }

    public Task SetNormalizedUserNameAsync(CustomUser user, string? normalizedName, CancellationToken cancellationToken)
    {
        // Implement as needed
        return Task.CompletedTask;
    }

    public Task SetUserNameAsync(CustomUser user, string? userName, CancellationToken cancellationToken)
    {
        
        user.UserName = userName ?? string.Empty;
        return Task.CompletedTask;
    }

    public Task<IdentityResult> UpdateAsync(CustomUser user, CancellationToken cancellationToken)
    {
        // Implement update logic if needed
        return Task.FromResult(IdentityResult.Success);
    }

    public void Dispose()
    {
        // Dispose resources if necessary
    }

    // IUserPasswordStore methods
    public Task<string?> GetPasswordHashAsync(CustomUser user, CancellationToken cancellationToken)
    {
        return _accountService.GetPasswordHashAsync(user, cancellationToken);
    }

    public Task<bool> HasPasswordAsync(CustomUser user, CancellationToken cancellationToken)
    {
        return Task.FromResult(!string.IsNullOrEmpty(user.PasswordHash));
        //return _accountService.HasPasswordAsync(user, cancellationToken);
    }

    public Task SetPasswordHashAsync(CustomUser user, string? passwordHash, CancellationToken cancellationToken)
    {
        user.PasswordHash = passwordHash;
        return Task.CompletedTask;
        //return _accountService.SetPasswordHashAsync(user, passwordHash, cancellationToken);
    }
}
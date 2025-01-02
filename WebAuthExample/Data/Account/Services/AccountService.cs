using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace WebAuthExample.Data.Account.Services;

public class AccountService : IAccountService
{
    private readonly ApplicationDbContext _dbContext;

    public AccountService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IdentityResult> CreateUserAsync(CustomUser user, CancellationToken cancellationToken)
    {
        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return IdentityResult.Success;
    }

    public async Task<IdentityResult> DeleteUserAsync(CustomUser user, CancellationToken cancellationToken)
    {
        _dbContext.Users.Remove(user);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return IdentityResult.Success;
    }

    public async Task<IdentityResult> UpdateUserAsync(CustomUser user, CancellationToken cancellationToken)
    {
        _dbContext.Users.Update(user);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return IdentityResult.Success;
    }

    public Task<CustomUser?> GetUserByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
    }

    public Task<CustomUser?> GetUserByNameAsync(string username, CancellationToken cancellationToken)
    {
        return _dbContext.Users.FirstOrDefaultAsync(u => u.UserName.ToUpper() == username, cancellationToken);
    }

    public async Task<string?> GetPasswordHashAsync(CustomUser user, CancellationToken cancellationToken)
    {
        var foundUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == user.Id, cancellationToken);
        return foundUser?.PasswordHash;
    }

    public async Task SetPasswordHashAsync(CustomUser user, string? passwordHash, CancellationToken cancellationToken)
    {
        var foundUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == user.Id, cancellationToken);
        if (foundUser == null) return;
        
        foundUser.PasswordHash = passwordHash ?? string.Empty;
    }

    public async Task<bool> HasPasswordAsync(CustomUser user, CancellationToken cancellationToken)
    {
        var foundUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == user.Id, cancellationToken);
        return !string.IsNullOrEmpty(foundUser?.PasswordHash);
    }

    public Task<string?> GetUserIdAsync(CustomUser user, CancellationToken cancellationToken)
    {
        return Task.FromResult(user.Id.ToString());
    }

    public Task<string?> GetUserNameAsync(CustomUser user, CancellationToken cancellationToken)
    {
        return Task.FromResult(user.UserName);
    }

    public async Task SetUserNameAsync(CustomUser user, string? userName, CancellationToken cancellationToken)
    {
        var foundUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == user.Id, cancellationToken);
        if (foundUser == null) return;
        foundUser.UserName = userName ?? string.Empty;
    }
}
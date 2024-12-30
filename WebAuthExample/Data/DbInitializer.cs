using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebAuthExample.Data.Account;

namespace WebAuthExample.Data;

internal static class DbInitializer
{
    public static async void InitializeDatabase(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        using var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        using var userStore = scope.ServiceProvider.GetRequiredService<IUserStore<ApplicationUser>>();
        using var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        
        // Ensure database is created
        if (context.Database.EnsureCreated())
        {
            var user = Activator.CreateInstance<ApplicationUser>();
            
            await userStore.SetUserNameAsync(user, "Admin", CancellationToken.None);
            var emailstore = (IUserEmailStore<ApplicationUser>)userStore;
            await emailstore.SetEmailAsync(user, "admin@admin.com", CancellationToken.None);
            
            await userManager.CreateAsync(user, "Admin1234+");
        }

        // Apply migrations if any
        context.Database.Migrate();
    }
}
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebAuthExample.Data.Account;

namespace WebAuthExample.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<CustomUser> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<CustomUser>(entity =>
        {
            entity.HasKey(u => u.Id);
            entity.Property(u => u.UserName).IsRequired().HasMaxLength(256);
            entity.Property(u => u.PreName).HasMaxLength(256);
            entity.Property(u => u.LastName).HasMaxLength(256);
            entity.Property(u => u.UserRole).IsRequired();
            entity.Property(u => u.PasswordHash).IsRequired().HasMaxLength(256); // Add this line
        });
    }
}
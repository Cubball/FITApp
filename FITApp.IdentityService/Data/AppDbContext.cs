using FITApp.IdentityService.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FITApp.IdentityService.Data;

public class AppDbContext : IdentityDbContext<User, Role, string>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Permission> Permissions { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<User>()
            .Property(u => u.RefreshToken)
            .HasDefaultValue(null)
            .HasMaxLength(1000);
        builder.Entity<User>()
            .Property(u => u.RefreshTokenExpiryTime)
            .HasDefaultValue(null);

        builder.Entity<Permission>()
            .HasIndex(p => p.Name)
            .IsUnique();
        builder.Entity<Permission>()
            .Property(p => p.Description)
            .HasMaxLength(500);
        builder.Entity<Permission>()
            .Property(p => p.Name)
            .HasMaxLength(100);
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using CleanArchitecture.Identity.Models;

namespace CleanArchitecture.Identity;

public class CleanArchitectureIdentityDbContext : IdentityDbContext
{
    public CleanArchitectureIdentityDbContext(DbContextOptions<CleanArchitectureIdentityDbContext> options) 
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }

    public virtual DbSet<RefreshToken>? RefreshTokens { get; set; }
    public virtual DbSet<ApplicationUser>? ApplicationUsers { get; set; }
}

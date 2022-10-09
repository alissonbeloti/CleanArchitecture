using CleanArchitecture.Domain;
using CleanArchitecture.Domain.Common;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Infrastructure.Persistence;

public class StreamerDbContext : DbContext
{
    public StreamerDbContext(DbContextOptions<StreamerDbContext> options): base(options)
    {

    }
    //protected override void OnConfiguring(DbContextOptionsBuilder options)
    //{
    //    options.UseSqlServer("Data Source=localhost;Initial Catalog=Streamer;" +
    //        "user id=sa; password=VaxiDrez2005$")
    //        .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information)
    //        .EnableSensitiveDataLogging();
    //}

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        CreateAddInformation();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        CreateAddInformation();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void CreateAddInformation()
    {
        foreach (var entry in ChangeTracker.Entries<BaseDomainModel>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedDate = DateTime.UtcNow;
                    entry.Entity.CreatedBy = "system";
                    break;

                case EntityState.Modified:
                    entry.Entity.LastModifiedDate = DateTime.UtcNow;
                    entry.Entity.LastModifiedBy = "system";
                    break;
            }
        }
    }

    //public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    //{
    //    foreach (var entry in ChangeTracker.Entries<BaseDomainModel>())
    //    {
    //        switch (entry.State)
    //        {
    //            case EntityState.Added:
    //                entry.Entity.CreatedDate = DateTime.UtcNow;
    //                entry.Entity.CreatedBy = "system";
    //                break;

    //            case EntityState.Modified:
    //                entry.Entity.LastModifiedDate = DateTime.UtcNow;
    //                entry.Entity.LastModifiedBy = "system";
    //                break;
    //        }
    //    }

    //    return await base.SaveChangesAsync(cancellationToken);
    //}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Streamer>()
            .HasMany(m => m.Videos)
            .WithOne(m => m.Streamer)
            .HasForeignKey(m => m.StreamerId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Video>()
            .HasMany(v => v.Atores)
            .WithMany(a => a.Videos)
            .UsingEntity(j => j.ToTable("VideoAtor"));
    }
    public DbSet<Streamer>? Streamers { get; set; }
    public DbSet<Video>? Videos { get; set; }
}

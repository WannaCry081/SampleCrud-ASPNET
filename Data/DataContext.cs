using Microsoft.EntityFrameworkCore;
using SampleCrud_ASPNET.Models.Entities;

namespace SampleCrud_ASPNET.Data;

public partial class DataContext(
    DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Token> Tokens { get; set; }
    public DbSet<Note> Notes { get; set; }

    public override Task<int> SaveChangesAsync(CancellationToken onCancellationToken = default)
    {
        UpdateTimestamps();
        return base.SaveChangesAsync(onCancellationToken);
    }

    private void UpdateTimestamps()
    {
        var entries = ChangeTracker
            .Entries<BaseEntity>()
            .Where(e => e.State == EntityState.Added ||
                        e.State == EntityState.Modified);

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAt = DateTime.Now;
            }
            entry.Entity.UpdatedAt = DateTime.Now;
        }
    }
}

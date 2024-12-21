using Microsoft.EntityFrameworkCore;
using SampleCrud_ASPNET.Models.Entities;

namespace SampleCrud_ASPNET.Data;

public partial class DataContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasMany(u => u.Tokens)
            .WithOne(t => t.User)
            .HasForeignKey(t => t.UserId);

        modelBuilder.Entity<User>()
            .HasIndex(u => new
            {
                u.Email,
                u.UserName
            }).IsUnique();

        modelBuilder.Entity<Token>()
            .HasIndex(t => new
            {
                t.Key
            }).IsUnique();

        modelBuilder.Entity<Note>(entity =>
        {
            entity.Property(e => e.Content)
                .HasColumnType("Text");
        });

        modelBuilder.Entity<Note>()
            .HasIndex(n => new
            {
                n.Title
            }).IsUnique();

        base.OnModelCreating(modelBuilder);
    }
}

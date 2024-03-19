using Microsoft.EntityFrameworkCore;
using Talpa_Api.Models;

namespace Talpa_Api.Contexts;

public class Context : DbContext
{
    public Context(DbContextOptions<Context> options) : base(options)
    {
        Database.EnsureCreated();
    }

    public virtual DbSet<Team> Teams { get; init; }

    public virtual DbSet<User> Users { get; init; }

    public virtual DbSet<Suggestion> Suggestions { get; init; }

    public virtual DbSet<Tag> Tags { get; init; }

    public virtual DbSet<Poll> Polls { get; init; }

    public virtual DbSet<Vote> Votes { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Suggestion>()
            .HasMany(s => s.Tags)
            .WithMany(t => t.Suggestions);

        modelBuilder.Entity<Suggestion>()
            .HasMany(s => s.Polls)
            .WithMany(p => p.Suggestions);
        
        modelBuilder.Entity<Team>()
            .HasOne(t => t.Poll)
            .WithOne(p => p.Team)
            .HasForeignKey<Poll>()
            .OnDelete(DeleteBehavior.NoAction);
    }
}
using Microsoft.EntityFrameworkCore;
using Talpa_Api.Models;

namespace Talpa_Api.Contexts;

public class Context : DbContext
{
    public Context(DbContextOptions<Context> options) : base(options)
    {
        Database.EnsureCreated();
    }

    public virtual DbSet<Team> Teams { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Suggestion> Suggestions { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    public virtual DbSet<Poll> Polls { get; set; }

    public virtual DbSet<Vote> Votes { get; set; }

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
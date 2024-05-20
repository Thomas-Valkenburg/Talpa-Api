using Microsoft.EntityFrameworkCore;
using Talpa_Api.Models;

namespace Talpa_Api.Contexts;

public class Context(DbContextOptions<Context> options) : DbContext(options)
{
	public DbSet<Team> Teams { get; init; }

    public DbSet<User> Users { get; init; }

    public DbSet<Suggestion> Suggestions { get; init; }

    public DbSet<Tag> Tags { get; init; }

    public DbSet<Poll> Polls { get; init; }

    public DbSet<PollDate> PollDates { get; init; }

    public DbSet<Vote> Votes { get; init; }

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
            .OnDelete(DeleteBehavior.Restrict);
    }
}
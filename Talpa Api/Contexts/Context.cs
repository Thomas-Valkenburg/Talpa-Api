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

    //public virtual DbSet<Poll> Polls { get; set; }

    public virtual DbSet<Vote> Votes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Suggestion>()
            .HasOne(p => p.Creator)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);
    }
}
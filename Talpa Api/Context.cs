using Microsoft.EntityFrameworkCore;
using Talpa_Api.Models;

namespace Talpa_Api;

public class Context(DbContextOptions<Context> options) : DbContext(options)
{
    public required DbSet<Team> Teams { get; init; }

    public required DbSet<User> Users { get; init; }

    public required DbSet<Suggestion> Suggestions { get; init; }

    public required DbSet<Tag> Tags { get; init; }
}
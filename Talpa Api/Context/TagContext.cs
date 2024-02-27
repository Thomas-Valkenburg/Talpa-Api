using Microsoft.EntityFrameworkCore;
using Talpa_Api.Models;

namespace Talpa_Api.Context;

public class TagContext(DbContextOptions<TagContext> options) : DbContext(options)
{
    public DbSet<Tag> Tags { get; init; } = null!;
}
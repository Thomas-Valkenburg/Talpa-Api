using Microsoft.EntityFrameworkCore;
using Talpa_Api.Models;

namespace Talpa_Api.Context;

public class TeamContext(DbContextOptions<TeamContext> options) : DbContext(options)
{
    public required DbSet<Team> Teams { get; init; }
}
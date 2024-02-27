using Microsoft.EntityFrameworkCore;
using Talpa_Api.Models;

namespace Talpa_Api.Context;

public class ManagerContext(DbContextOptions<ManagerContext> options) : DbContext(options)
{
    public DbSet<Manager> Managers { get; init; } = null!;
}
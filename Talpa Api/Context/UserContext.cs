using Talpa_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Talpa_Api.Context
{
    public class UserContext(DbContextOptions<UserContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; init; } = null!;
    }
}

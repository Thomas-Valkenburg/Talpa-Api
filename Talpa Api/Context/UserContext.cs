using Microsoft.EntityFrameworkCore;
using Talpa_Api.Models;

namespace Talpa_Api.Context;

public class UserContext(DbContextOptions<UserContext> options) : DbContext(options)
{
    public required DbSet<User> Users { get; init; }
}
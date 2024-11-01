using fsacwapi.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace fsacwapi.Infrastructure.Data;

public class DBContext : DbContext
{
    public DBContext(DbContextOptions<DBContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Server=localhost;Database=fsacwapi;User Id=postgres;Password=root;");
    }

    public DbSet<User> Users { get; set; }
}

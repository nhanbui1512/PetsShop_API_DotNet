
using Microsoft.EntityFrameworkCore;
using petshop.Models;
using PetsShop_API_DotNet.Models;

namespace petshop.Data
{
  public class AppDbContext : DbContext
  {
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      // Tắt logging
      optionsBuilder.UseLoggerFactory(null);
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Role> Roles { get; set; }

  }
}
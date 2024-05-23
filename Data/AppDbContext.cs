
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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      modelBuilder.Entity<Product>()
          .HasMany(p => p.Options)
          .WithOne(c => c.Product)
          .HasForeignKey(c => c.ProductId)
          .OnDelete(DeleteBehavior.Cascade); // Thiết lập xóa theo kiểu Cascade

      modelBuilder.Entity<Role>()
               .HasMany(p => p.Users)
               .WithOne(c => c.Role)
               .HasForeignKey(c => c.RoleId)
               .OnDelete(DeleteBehavior.Cascade); // Thiết lập xóa theo kiểu Cascade

    }

    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Option> Options { get; set; }



  }
}

using Microsoft.EntityFrameworkCore;
using petshop.Models;

namespace petshop.Data
{
  public class AppDbContext : DbContext
  {
    public AppDbContext(DbContextOptions <AppDbContext> options) : base(options){

    }

    public DbSet <User> Users { get; set; }
    
  }
}
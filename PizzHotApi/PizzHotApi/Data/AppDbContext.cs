using Microsoft.EntityFrameworkCore;
using PizzHotApi.Models;

namespace PizzHotApi.Data;


public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options) { }
    public DbSet<Pizza> Pizzas { get; set; } = null!;
}

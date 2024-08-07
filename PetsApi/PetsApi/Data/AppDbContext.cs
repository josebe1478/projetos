using Microsoft.EntityFrameworkCore;
using PetsApi.Models;

namespace PetsApi.Data;


public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options) { }
    public DbSet<Pets> Petss { get; set; } = null!;
}

using BulkyBookWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace BulkyBookWeb.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):
        base(options)
    {
        Database.EnsureCreated();
    }
    public DbSet<Category> Categories { get; set; }
}

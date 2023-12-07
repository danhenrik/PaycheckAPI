using BreakEven.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace BreakEven.API.Data;

public class AppDbContext: DbContext
{
    public AppDbContext(DbContextOptions options) : base(options) {}
    
    public DbSet<Employee> Employees { get; set; }
}
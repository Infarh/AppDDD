using AppDDD.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AppDDD.DAL.Context;

public class AppDB : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Departament> Departaments { get; set; }

    public AppDB(DbContextOptions<AppDB> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder model)
    {
        base.OnModelCreating(model);

        model.Entity<Product>()
           .Property(p => p.Price)
           .HasPrecision(18, 2);

        model.Entity<Employee>()
           .Property(p => p.Salary)
           .HasPrecision(18, 2);
    }
}

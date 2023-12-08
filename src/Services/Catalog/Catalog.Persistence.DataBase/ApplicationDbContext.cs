using Catalog.Domain;
using Catalog.Persistence.DataBase.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Persistence.DataBase;

public class ApplicationDbContext:DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options)
    {
        
    }
    public DbSet<Product>Products { get; set; }
    public DbSet<ProductInStock> ProductInStocks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema("Catalog");
        ModelConfig(modelBuilder);
    }
    private void ModelConfig(ModelBuilder modelBuilder)
    {
        new ProductConfiguration(modelBuilder.Entity<Product>());
        new ProductInStockConfiguration(modelBuilder.Entity<ProductInStock>());
    }
}

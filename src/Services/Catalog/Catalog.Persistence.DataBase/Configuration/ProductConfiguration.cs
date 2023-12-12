using Catalog.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Persistence.DataBase.Configuration
{
    public class ProductConfiguration
    {
        public ProductConfiguration(EntityTypeBuilder<Product>entityBuilder)
        {
            entityBuilder.HasIndex(c => c.ProductId);
            entityBuilder.Property(c=>c.Name)
                .IsRequired()
                .HasMaxLength(100);
            entityBuilder.Property(c=>c.Description)
                .IsRequired().HasMaxLength(500);

            var products=new List<Product>();
            var random = new Random();
            for (int i = 1; i < 100; i++)
            {
                products.Add(
                    new Product
                    {
                        ProductId = i,
                        Name=$"Product {i}",
                        Description=$"Description for product {i}",
                        Price = random.Next(100,1000),
                            
                    });
            }
            entityBuilder.HasData(products);
        }
    }
}

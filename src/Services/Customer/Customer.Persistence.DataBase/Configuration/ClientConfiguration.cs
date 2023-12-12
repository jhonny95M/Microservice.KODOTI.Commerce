using Customer.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Customer.Persistence.DataBase.Configuration;

public sealed class ClientConfiguration : IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> builder)
    {
        builder.HasKey(c => c.ClientId);
        builder.Property(c => c.Name).IsRequired().HasMaxLength(100);
        var clients= new List<Cliente>();
        for (int i = 1; i < 10; i++)
        {
            clients.Add(new Cliente
            {
                ClientId=i,
                Name=$"Client {i}"
            });
        }
        builder.HasData(clients);
    }
}

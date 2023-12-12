using Identity.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Persistence.DataBase.Configuration
{
    public class ApplicationRoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationRole> builder)
        {
            builder.HasKey(x=>x.Id);
            builder.HasData(new ApplicationRole
            {
                Id=Guid.NewGuid().ToString().ToLower(),
                Name="Admin",
                NormalizedName="ADMIN"
            });
            builder.HasMany(e=>e.UserRoles).WithOne(e=>e.Role).HasForeignKey(e=>e.RoleId).IsRequired();
        }
    }
}

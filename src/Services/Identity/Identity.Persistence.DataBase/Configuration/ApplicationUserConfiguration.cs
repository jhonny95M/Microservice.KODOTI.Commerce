using Identity.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Persistence.DataBase.Configuration
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.UserName).IsRequired().HasMaxLength(100);
            builder.Property(x=>x.LastName).IsRequired().HasMaxLength(100);
            builder.HasMany(e=>e.UserRoles).WithOne(e=>e.User).HasForeignKey(e=>e.UserId).IsRequired();
        }
    }
}

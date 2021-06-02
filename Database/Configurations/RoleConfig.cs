using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Configurations
{
    public class RoleConfig: IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole{Name = "Super Administrator",NormalizedName = "SUPER ADMINISTRATOR" },
                new IdentityRole{Name = "Administrator", NormalizedName = "ADMINISTRATOR" }
            );
        }
    }
}
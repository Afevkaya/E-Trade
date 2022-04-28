using E_Trade.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Trade.Repository.Seeds
{
    public class AppRoleSeed : IEntityTypeConfiguration<AppRole>
    {
        public void Configure(EntityTypeBuilder<AppRole> builder)
        {
            builder.HasData(
            new AppRole
            {
                Name = "Admin",
                NormalizedName = "ADMIN"
            },
            new AppRole
            {
                Name = "Customer",
                NormalizedName = "CUSTOMER"
            },
            new AppRole
            {
                Name = "Customer Service",
                NormalizedName = "CUSTOMER SERVICE"
            },
            new AppRole
            {
                Name = "Sales Person",
                NormalizedName = "SALES PERSON"
            });
        }
    }
}

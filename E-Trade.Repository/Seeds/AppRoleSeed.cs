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
                Name = "Admin"
            },
            new AppRole
            {
                Name = "Customer"
            },
            new AppRole
            {
                Name = "Customer Service"
            });
        }
    }
}

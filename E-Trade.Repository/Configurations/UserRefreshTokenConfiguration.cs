using E_Trade.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Trade.Repository.Configurations
{
    // Database tarafında UserRefreshToken tablosu için config işlemlerinin yapıldığı class.
    // Config işlemi entity class üzerinde yapılır.
    // Config class olabilmesi için IEntityTypeConfiguration<> generic interface'ini implement edilmesi gerekir.
    // Database tarafına yansıması için Context class2ı içinde belirtilmesi gerekir.

    // UserRefreshTokenConfiguration class.
    public class UserRefreshTokenConfiguration : IEntityTypeConfiguration<UserRefreshToken>
    {
        public void Configure(EntityTypeBuilder<UserRefreshToken> builder)
        {
            builder.HasKey(x=>x.UserId);
            builder.Property(x => x.Code).IsRequired().HasMaxLength(200);

            builder.ToTable("UserRefreshTokens");
        }
    }
}

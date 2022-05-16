using E_Trade.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Trade.Repository.Configurations
{
    // Database tarafında product tablosu için config işlemlerinin yapıldığı class.
    // Config işlemi entity class üzerinde yapılır.
    // Config class olabilmesi için IEntityTypeConfiguration<> generic interface'ini implement edilmesi gerekir.
    // Database tarafına yansıması için Context class2ı içinde belirtilmesi gerekir.

    // ProductConfiguration class.
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Price).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(x => x.Description).IsRequired().HasMaxLength(300);
            builder.Property(x => x.ImageUrl).IsRequired();
            builder.Property(x => x.StockQuantity).IsRequired();

            builder.ToTable("Products");

            builder.HasOne(x => x.Category).WithMany(x => x.Products).HasForeignKey(x => x.CategoryId);

        }
    }
}

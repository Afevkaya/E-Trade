using E_Trade.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Trade.Repository.Configurations
{
    // Database tarafında Category tablosu için config işlemlerinin yapıldığı class.
    // Config işlemi entity class üzerinde yapılır.
    // Config class olabilmesi için IEntityTypeConfiguration<> generic interface'ini implement edilmesi gerekir.
    // Database tarafına yansıması için Context class2ı içinde belirtilmesi gerekir.

    // CategoryConfiguration class.
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);

            builder.ToTable("Categories");
        }
    }
}

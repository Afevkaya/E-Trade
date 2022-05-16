using E_Trade.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Trade.Repository.Seeds
{
    // Database oluşturulurken Db'ye eklenmesi istenilen datalar için oluşturulan class.
    // Bu class da bir config class'ı olduğu için IEntityTypeConfiguration<> interface'ini implement etmesi gerekir.
    // Db ye yansıması için Context class'ına bu class'ın bildirilmesi gerekir. 

    // CategroySeed class
    public class CategorySeed : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(
                new Category { Id = 1, Name = "Elektronik" },
                new Category { Id = 2, Name = "Beyaz Eşya" });
        }
    }
}

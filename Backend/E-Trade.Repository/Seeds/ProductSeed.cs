using E_Trade.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Trade.Repository.Seeds
{
    // Database oluşturulurken Db'ye eklenmesi istenilen datalar için oluşturulan class.
    // Bu class da bir config class'ı olduğu için IEntityTypeConfiguration<> interface'ini implement etmesi gerekir.
    // Db ye yansıması için Context class'ına bu class'ın bildirilmesi gerekir. 

    // ProductSeed class
    public class ProductSeed : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(
                new Product
                {
                    Id = 1,
                    Name = "Bilgisayar",
                    Price = 10000,
                    Description = "Bilgisayar",
                    ImageUrl = "https://images.unsplash.com/photo-1603302576837-37561b2e2302?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1468&q=80",
                    StockQuantity = 100,
                    CategoryId = 1
                }, new Product
                {
                    Id = 2,
                    Name = "Mouse",
                    Price = 300,
                    Description = "Mouse",
                    ImageUrl = "https://images.unsplash.com/photo-1563297007-0686b7003af7?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1517&q=80",
                    StockQuantity = 200,
                    CategoryId = 1
                }, new Product
                {
                    Id = 3,
                    Name = "Buzdolabı",
                    Price = 7000,
                    Description = "Buzdolabı",
                    ImageUrl = "https://images.unsplash.com/photo-1536353284924-9220c464e262?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1471&q=80",
                    StockQuantity = 150,
                    CategoryId = 2
                }, new Product
                {
                    Id = 4,
                    Name = "Derin Dondurucu",
                    Price = 6000,
                    Description = "Derin Dondurucu",
                    ImageUrl = "https://images.unsplash.com/photo-1584568694244-14fbdf83bd30?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=726&q=80",
                    StockQuantity = 140,
                    CategoryId = 2,
                });
        }
    }
}

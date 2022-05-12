using E_Trade.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace E_Trade.Repository
{
    // Database tabloları ile entity class'larını birbirine maplediğimiz class.
    // Identity işlemleri kullanılacağı için IdentityDbContext class'ından inheritance alınıyor.

    // ETradeDbContext class
    public class ETradeDbContext : IdentityDbContext<AppUser,AppRole,string>
    {
        // Db config işlemleri
        public ETradeDbContext(DbContextOptions<ETradeDbContext> options) : base(options)
        {

        }

        // Maplenilen bölüm
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<UserRefreshToken> UserRefreshTokens { get; set; }
        public DbSet<Basket> Baskets { get; set; }


        // Db tablo config işlemleri
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}

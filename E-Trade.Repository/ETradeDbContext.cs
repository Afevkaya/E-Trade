﻿using E_Trade.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace E_Trade.Repository
{
    public class ETradeDbContext : DbContext
    {
        public ETradeDbContext(DbContextOptions<ETradeDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}

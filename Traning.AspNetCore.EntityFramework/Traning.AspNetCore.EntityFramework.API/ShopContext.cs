using Microsoft.EntityFrameworkCore;
using System;
using Traning.AspNetCore.EntityFramework.Data;
using Traning.AspNetCore.EntityFramework.Data.Models;

namespace Traning.AspNetCore.EntityFramework.API
{
    public class ShopContext : DbContext, IShopContext
    {
        public ShopContext(DbContextOptions<ShopContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductReview> ProductReviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<Product>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Product>()
                .Property(x => x.SaleFrom)
                .HasConversion(x => x, x => x.HasValue ? DateTime.SpecifyKind(x.Value, DateTimeKind.Utc) : x);
            modelBuilder.Entity<Product>()
                .HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<Product>()
                .HasMany(x => x.Reviews)
                .WithOne()
                .HasForeignKey(x => x.ProductId);

            modelBuilder.Entity<ProductReview>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<ProductReview>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();
        }
    }
}

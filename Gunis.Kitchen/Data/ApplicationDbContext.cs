using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

using Gunis.Kitchen.Models;
using Gunis.Kitchen.Models.Enums;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Gunis.Kitchen.Data
{
    public class ApplicationDbContext
           : IdentityDbContext<MyIdentityUser, MyIdentityRole, Guid>
    {
        public DbSet<Item> Items { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }



        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            var sizeConverter = new ValueConverter<ProductSizes, string>(
               v => v.ToString()
               , v => (ProductSizes)Enum.Parse(typeof(ProductSizes), v));

            base.OnModelCreating(builder);
            builder.Entity<Category>()
                  .Property(e => e.CreatedAt)
                  .HasDefaultValueSql("getdate()");

            builder.Entity<Item>()
                   .Property(e => e.ItemPrice)
                   .HasPrecision(precision:7 , scale: 2);

            builder.Entity<Item>()
                   .Property(e => e.ItemSize)
                   .HasConversion(sizeConverter);

        }
    }
}

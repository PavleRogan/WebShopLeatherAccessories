using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Domain.Entities;

namespace WebShop.Infrastructure.Persistence
{
    internal class WebShopDbContext : DbContext
    {

        public WebShopDbContext(DbContextOptions<WebShopDbContext> options) : base(options) { 
        
        
        }

        internal DbSet<User> Users { get; set; }

        internal DbSet<Admin> Admins { get; set; }

        internal DbSet<Order> Orders  { get; set; }

        internal DbSet<Product> Products  { get; set; }

        internal DbSet<OrderItem> OrderItems { get; set; }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasMany(u => u.Orders)
                      .WithOne(o => o.User)
                      .HasForeignKey(o => o.UserId);

                entity.OwnsOne(u => u.Address);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasMany<OrderItem>(o => o.OrderItems)
                      .WithOne(oi => oi.Order)
                      .HasForeignKey(oi => oi.OrderId);
                
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasMany<OrderItem>(p => p.OrderItems)
                .WithOne(oi => oi.Product)
                .HasForeignKey(oi => oi.ProductId);
            });
            
         }

    }
}

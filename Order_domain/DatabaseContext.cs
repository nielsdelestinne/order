using Microsoft.EntityFrameworkCore;
using Order_domain.Customers;
using Order_domain.Items;
using Order_domain.Orders;
using Order_domain.Orders.OrderItems;
using System;
using System.Collections.Generic;
using System.Text;

namespace Order_domain
{
    public class DatabaseContext : DbContext 
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("MyInMemoryDB");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Customer>()
                    .HasMany(customer => customer.Orders);

            modelBuilder
                .Entity<Order>()
                    .HasMany(order => order.OrderItems);

            modelBuilder
                .Entity<Order>()
                    .HasOne(order => order.Customer)
                        .WithMany(customer => customer.Orders)
                            .HasForeignKey(order => order.CustomerId);

            modelBuilder
                .Entity<OrderItem>()
                    .HasOne(orderItem => orderItem.Item)
                        .WithMany()
                            .HasForeignKey(orderItem => orderItem.ItemId);
                    
        }
    }
}

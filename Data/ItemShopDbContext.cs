using System;
using System.Collections.Generic;
using System.Text;
using ItemShop.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ItemShop.Data
{
    public class ItemShopDbContext : IdentityDbContext<User>
    {
        public ItemShopDbContext(DbContextOptions<ItemShopDbContext> options)
            : base(options)
        {}
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Transaction>()
                .HasOne(t => t.Customer)
                .WithMany(t => t.Transactions)
                .HasForeignKey(t => t.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Transaction>()
                .HasOne(t => t.Product)
                .WithMany(t => t.Transactions)
                .HasForeignKey(t => t.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Transaction>()
                .HasOne(t => t.Invoice)
                .WithMany(t => t.Transactions)
                .HasForeignKey(t => t.InvoiceId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Invoice> Invoices { get; set; }

        public DbSet<Transaction> Transactions { get; set; }
    }
}

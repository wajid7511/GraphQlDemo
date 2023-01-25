using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using Database.Configurations;
using Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Database
{
    public class GraphQlDatabaseContext : DbContext
    {
        public GraphQlDatabaseContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new GroceryConfiguration());
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost; Database=GraphQlDemo; User ID=SA;Password=P@ssw0rd.W;TrustServerCertificate=true;");
            }
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Grocery> Groceries { get; set; }
    }
}


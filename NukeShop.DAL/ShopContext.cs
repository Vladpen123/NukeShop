using Microsoft.EntityFrameworkCore;
using NukeShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NukeShop.DAL
{
    public class ShopContext : DbContext
    {
        public ShopContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Product>? Products { get; set; }
        public DbSet<Category>? Categories { get; set; }
        public DbSet<Manufacturer>? Manufacturers { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetProperties())
                .Where(p => p.ClrType == typeof(string)))
            {
                property.SetColumnType("nvarchar(150)");
            }

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ShopContext).Assembly);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes()
              .SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Cascade;
            }
               

            Category c1 = new() { Id = 1, Name = "Кроссовки" };
            Category c2 = new() { Id = 2, Name = "Футболки" };
            Category c3 = new() { Id = 3, Name = "Шорты" };
            Category c4 = new() { Id = 4, Name = "Аксессуары" };

            Manufacturer m1 = new() { Id = 1, Name = "Nike" };
            Manufacturer m2 = new() { Id = 2, Name = "Adidas" };
            Manufacturer m3 = new() { Id = 3, Name = "Puma" };
            Manufacturer m4 = new() { Id = 4, Name = "Helly Henson" };


            modelBuilder.Entity<Category>().HasData(c1,c2,c3,c4);
            modelBuilder.Entity<Manufacturer>().HasData(m1, m2, m3, m4);
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "REACT VAPOR NXT CLY",
                    Articul = "CV0726-007",
                    Count = 23,
                    Description = "КРОССОВКИ NIKE M REACT VAPOR NXT CLY АРТИКУЛ: CV0726-007",
                    ManufacturerId = 1,
                    CategoryId = 1,
                    Price = 7379.00m,
                    Photo = null
                }
        );
        }
    }

}


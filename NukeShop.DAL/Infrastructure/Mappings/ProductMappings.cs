using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NukeShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NukeShop.DAL.Infrastructure.Mappings
{
    public class ProductMappings : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name).IsRequired().HasColumnType("nvarchar(150)");
            builder.Property(p => p.Articul).IsRequired().HasColumnType("nvarchar(10)");
            builder.Property(p => p.Price).IsRequired().HasColumnType("decimal(19,2)");
            builder.Property(p => p.Count).IsRequired().HasColumnType("int");
            builder.Property(p => p.Photo);
            builder.Property(p => p.Description).HasColumnType("nvarchar(max)");

            builder.Property(p => p.CategoryId).IsRequired();
            builder.Property(p => p.ManufacturerId).IsRequired();

            builder.ToTable("Products");



        }
    }
}

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
    public class ManufacturerMapping : IEntityTypeConfiguration<Manufacturer>
    {
        public void Configure(EntityTypeBuilder<Manufacturer> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name).IsRequired().HasColumnName("nvarchar(150)");

            //relation 1:M
            builder.HasMany(p => p.Products).WithOne(m=>m.Manufacturer).HasForeignKey(m => m.ManufacturerId);

            builder.ToTable("Manufacturers");
         }
    }
}

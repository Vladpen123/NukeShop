using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NukeShop.API.Models.DTOs
{
    public class ProductResultDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[]? Photo { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
        public string Articul { get; set; }
        public int CategoryId { get; set; }
        public int ManufacturerId { get; set; }

        public string CategoryName { get; set; }
        public string ManufacturerName { get; set; }

    }
}


using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace NukeShop.Domain.Models
{
    public class Product : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[]? Photo { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
  
        public string Articul { get; set; }

        public int CategoryId { get; set; }
        public int ManufacturerId { get; set; }

        public Category Category { get; set; }

        public  Manufacturer Manufacturer { get; set; }
       


    }
}

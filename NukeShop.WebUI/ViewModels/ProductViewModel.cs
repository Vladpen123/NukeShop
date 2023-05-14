using Microsoft.AspNetCore.Mvc.Rendering;
using NukeShop.BLL.DTOs.CategoryDtos;
using NukeShop.BLL.DTOs.ManufacturerDtos;
using System.ComponentModel.DataAnnotations;

namespace NukeShop.WebUI.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter the product name")]
        [StringLength(150, MinimumLength = 2, ErrorMessage = "The product name must be at least 2 characters long")]
        public string? Name { get; set; }

        [Required]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Please enter the articul")]
        [StringLength(10, MinimumLength = 6, ErrorMessage = "Articul must be between 6 and 10 characters long")]
        public string? Articul { get; set; }
        [Required(ErrorMessage = "Please enter the count")]
        public int Count { get; set; }
        public string? Description { get; set; }
        public IFormFile? Photo { get; set; }
        public int CategoryId { get; set; }
        public int ManufacturerId { get; set; }

        // public string CategoryName { get; set; }
        // public string ManufacturerName { get; set; }

    }
}

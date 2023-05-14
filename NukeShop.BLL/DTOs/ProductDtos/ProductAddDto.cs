using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NukeShop.BLL.DTOs.ProductDtos
{
    public class ProductAddDto
    {

        [Required(ErrorMessage = "The field {0} is required")]
        [StringLength(150, ErrorMessage = "The field {0} must be between {2} and {1} characters", MinimumLength = 2)]
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[]? Photo { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
        [Required(ErrorMessage = "The field {0} is required")]
        [StringLength(10, ErrorMessage = "The field {0} must be between {2} and {1} characters", MinimumLength = 6)]
        public string Articul { get; set; }
        [Required(ErrorMessage = "The field {0} is required")]
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "The field {0} is required")]
        public int ManufacturerId { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace NukeShop.WebUI.ViewModels
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter the category name")]
        [StringLength(150, MinimumLength = 2, ErrorMessage = "The category name must be at least 2 characters long")]
        public string Name { get; set; }
        public List<ProductViewModel>? Products { get; set; }
    }
}

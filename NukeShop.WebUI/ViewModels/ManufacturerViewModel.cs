using System.ComponentModel.DataAnnotations;

namespace NukeShop.WebUI.ViewModels
{
    public class ManufacturerViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter the manufacturer name")]
        [StringLength(150, MinimumLength = 2, ErrorMessage = "The manufacturer name must be at least 2 characters long")]
        public string Name { get; set; }
    }
}

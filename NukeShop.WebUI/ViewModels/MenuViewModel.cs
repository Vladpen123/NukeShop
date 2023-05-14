using NukeShop.BLL.DTOs.CategoryDtos;
using NukeShop.BLL.DTOs.ManufacturerDtos;
using NukeShop.BLL.DTOs.ProductDtos;

namespace NukeShop.WebUI.ViewModels
{
    public class MenuViewModel
    {
        public List<CategoryDto> Categories { get; set; }
        public List<ManufacturerDto> Manufacturers { get; set; }

    }
}

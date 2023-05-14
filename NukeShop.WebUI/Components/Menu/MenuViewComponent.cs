using Microsoft.AspNetCore.Mvc;
using NukeShop.BLL.Services;
using NukeShop.WebUI.ViewModels;

namespace NukeShop.WebUI.Components.Menu
{
    public partial class MenuViewComponent : ViewComponent
    {
        private readonly IManufacturerService _manufacturerService;
        private readonly ICategoryService _categoryService;
        public MenuViewComponent(IManufacturerService manufacturerService, ICategoryService categoryService)
        {
            _manufacturerService = manufacturerService;
            _categoryService = categoryService;

        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _categoryService.GetAll();
            var manufacturers = await _manufacturerService.GetAll();

            MenuViewModel model = new()
            {
                Categories = categories,
                Manufacturers = manufacturers
            };

            return View(model);
        }
    }
}
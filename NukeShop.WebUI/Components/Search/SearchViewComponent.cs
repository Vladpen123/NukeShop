using Microsoft.AspNetCore.Mvc;
using NukeShop.BLL.Services;
using NukeShop.WebUI.Models;

namespace NukeShop.WebUI.Components.Search
{
    public partial class SearchViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
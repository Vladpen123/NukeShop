using Microsoft.AspNetCore.Mvc;

namespace NukeShop.WebUI.Components.Favourites
{
    public partial class FavouritesViewComponent : ViewComponent
    {
        public FavouritesViewComponent()
        {

        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
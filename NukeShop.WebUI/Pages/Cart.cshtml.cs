using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NukeShop.BLL.Services;
using NukeShop.WebUI.Infrastructure;
using NukeShop.WebUI.Models;

namespace NukeShop.WebUI.Pages
{
    public class CartModel : PageModel
    {
        private IProductService productService;

        public CartModel(IProductService productService)
        {
            this.productService = productService;
        }

        public Cart? Cart { get; set; }
        public string ReturnUrl { get; set; }

        public void OnGet(string returnUrl)
        {
            ReturnUrl = returnUrl;
            Cart = HttpContext.Session.GetObjectFromJson<Cart>("cart") ?? new Cart();
        }

        public async Task<IActionResult> OnPost(int id, string returnUrl)
        {
            var product = await productService.Get(id);
            if (product != null)
            {
                Cart = HttpContext.Session.GetObjectFromJson<Cart>("cart") ?? new Cart();
                Cart.AddItem(product, 1);
                HttpContext.Session.SetObjectAsJson("cart", Cart);
            }
            return RedirectToPage(new {returnUrl });
        }
    }
}

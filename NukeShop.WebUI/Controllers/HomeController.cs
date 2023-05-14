using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NukeShop.BLL.DTOs.ProductDtos;
using NukeShop.BLL.DTOs;
using NukeShop.BLL.Services;
using NukeShop.WebUI.Models;
using System.Diagnostics;
using NukeShop.BLL.Utils.QueryParameters;
using NukeShop.WebUI.ModelBinders;

namespace NukeShop.WebUI.Controllers
{



    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly IProductService _productService;
        public HomeController(IProductService productService)
        {
            _productService = productService;
        }




        [HttpGet]
        public async Task<IActionResult> Index([ModelBinder(BinderType = typeof(MenuModelBinder))] MenuSearch? search)
        {
            var pp = search?.MenuSearchToProductParameters();

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                var nextProducts = await _productService.Search(pp);

                return nextProducts.MetaData!.CurrentPage < nextProducts.MetaData.PageSize
                    ? PartialView("_ProductCard", nextProducts) : NoContent();
            }
            var model = await _productService.Search(pp);
            return View(model);

        }



        [HttpGet]
        public async Task<IActionResult> Product(int id) =>
            View(await _productService.Get(id));



    }
}
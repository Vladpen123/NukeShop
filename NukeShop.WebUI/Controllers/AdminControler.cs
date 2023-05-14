using NukeShop.WebUI.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NukeShop.BLL.DTOs;
using NukeShop.BLL.DTOs.CategoryDtos;
using NukeShop.BLL.DTOs.ManufacturerDtos;
using NukeShop.BLL.DTOs.ProductDtos;
using NukeShop.BLL.Services;
using NukeShop.BLL.Utils.QueryParameters;
using NukeShop.WebUI.ViewModels;
using System.Diagnostics;
using System.IO;

namespace NukeShop.WebUI.Controllers
{
    [Authorize(Roles = "admin")]
    [IgnoreAntiforgeryToken]
    public class AdminController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IManufacturerService _manufacturerService;
        private readonly IWebHostEnvironment _appEnvironment;

        
        public AdminController(IProductService productService,
            ICategoryService categoryService,
            IManufacturerService manufacturerService,
            IWebHostEnvironment appEnvironment)
        {
            _productService = productService;
            _categoryService = categoryService;
            _manufacturerService = manufacturerService;
            _appEnvironment = appEnvironment;
        }

        #region Product

        [HttpGet]
        public async Task<IActionResult> ProductList(int? id)
        {
            int page = id ?? 0;
            ViewBag.Cats = new SelectList(await _categoryService.GetAll(), "Id", "Name");
            ViewBag.Mans = new SelectList(await _manufacturerService.GetAll(), "Id", "Name");
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                var nextProducts = await GetItemsPage(page);

                return PartialView("_ProductItems", nextProducts);

            }
            PagingResponse<ProductDto> model = await _productService.Search(new ProductParameters());
      


            return View(model);
        }

        private async Task<PagingResponse<ProductDto>> GetItemsPage(int page = 1, int count = 10) =>
            await _productService.Search(new ProductParameters() { PageNumber = page, PageSize = count });

        [HttpGet]
        public IActionResult AddProduct()
        {

            return PartialView("_AddProduct");
        }

        [HttpPost]
     
        public async Task<IActionResult> AddProduct(ProductViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                ModelState.Clear();
                return PartialView("_AddProduct", vm);
            }

            byte[]? imageData;
            if (vm.Photo != null)
            {
                imageData = ExtenionsMethods.IFormFileToByteArray(vm.Photo);
                if(imageData.Length > 2097152)
                {
                    ModelState.AddModelError("File", "The file is too large.");
                }
            }
               
            else
            {
                string defaultImagePath = _appEnvironment.WebRootPath + @"\images\productNoImage.jpg";
                imageData = System.IO.File.ReadAllBytes(defaultImagePath);
            }

            var product = new ProductAddDto
            {
                CategoryId = vm.CategoryId,
                ManufacturerId = vm.ManufacturerId,
                Price = vm.Price,
                Count = vm.Count,
                Description = vm.Description,
                Articul = vm.Articul,
                Name = vm.Name,
                Photo = imageData
            };
            await _productService.Post(product);

            return RedirectToAction("ProductList");
        }

        [HttpGet]
        public async Task<IActionResult> EditProduct(int id)
        {

            if (id != 0)
            {
                var product = await _productService.Get(id);

            

                var vm = new ProductViewModel
                {
                    CategoryId = product.CategoryId,
                    ManufacturerId = product.ManufacturerId,
                    Articul = product.Articul,
                    Price = product.Price,
                    Count = product.Count,
                    Description = product.Description,
                    //Photo = formFile,
                    Name = product.Name,
                    Id = product.Id

                };
                return PartialView("_EditProduct", vm);
            }
            return NotFound();


        }

        [HttpPost]
       
        public async Task<IActionResult> EditProduct(ProductViewModel vm)
        {
            if (!ModelState.IsValid)
                return PartialView("_EditProduct", vm);

            var dto = new ProductEditDto
            {
                Id = vm.Id,
                CategoryId = vm.CategoryId,
                ManufacturerId = vm.ManufacturerId,
                Price = vm.Price,
                Count = vm.Count,
                Description = vm.Description,
                Articul = vm.Articul,
                Name = vm.Name
            };

            if (vm.Photo != null)
                dto.Photo = ExtenionsMethods.IFormFileToByteArray(vm.Photo!);
            else
            {
                string defaultImagePath = _appEnvironment.WebRootPath + @"\images\productNoImage.jpg";
                dto.Photo = System.IO.File.ReadAllBytes(defaultImagePath);
            }



            await _productService.Put(dto);


            return RedirectToAction("ProductList");
        }



        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDeleteProduct(int? id)
        {
            if (id != null)
            {
                var dto = await _productService.Get(id.Value);
                if (dto != null)
                {
                    return View(dto);
                }
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProduct(int? id)
        {
            if (id != null)
            {
                var dto = await _productService.Get(id.Value);
                if (dto != null)
                {
                    await _productService.Delete(id.Value);
                    return RedirectToAction("ProductList");
                }
            }

            return NotFound();
        }

        #endregion

        #region Category

        [HttpGet]
        public async Task<IActionResult> CategoryList() =>
            View(await _categoryService.GetAll());

        [HttpGet]
        public IActionResult AddCategory() =>
            PartialView("_AddCategory");

        [HttpPost]
       
        public async Task<IActionResult> AddCategory(CategoryViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                ModelState.Clear();
                return PartialView("_AddCategory", vm);
            }


            var category = new CategoryAddDto()
            {
                Name = vm.Name,
            };
            await _categoryService.Post(category);

            return RedirectToAction("CategoryList");
        }

        [HttpGet]
        public async Task<IActionResult> CategoryDetails(int id)
            => View(await _categoryService.GetWithProducts(id));



        [HttpGet]
        public async Task<IActionResult> EditCategory(int id)
        {

            if (id != 0)
            {
                var category = await _categoryService.Get(id);

                var vm = new CategoryViewModel
                {
                    Name = category.Name,
                    Id = category.Id

                };
                return PartialView("_EditCategory", vm);
            }
            return NotFound();


        }

        [HttpPost]
       
        public async Task<IActionResult> EditCategory(CategoryViewModel vm)
        {
             if (!ModelState.IsValid)
                return PartialView("_EditCategory", vm);

            var dto = new CategoryEditDto
            {
                Id = vm.Id,
                Name = vm.Name,

            };
            await _categoryService.Put(dto);

            return RedirectToAction("CategoryList");
        }



        [HttpGet]
        [ActionName("DeleteCategory")]
        public async Task<IActionResult> ConfirmDeleteCategory(int? id)
        {
            if (id != null)
            {
                var dto = await _categoryService.Get(id.Value);
                if (dto != null)
                {
                    return View(dto);
                }
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCategory(int? id)
        {
            if (id != null)
            {
                var dto = await _categoryService.Get(id.Value);
                if (dto != null)
                {
                    await _categoryService.Delete(id.Value);
                    return RedirectToAction("CategoryList");
                }
            }

            return NotFound();
        }

        #endregion

        #region Manufacturer
        [HttpGet]
        public async Task<IActionResult> ManufacturerList()
        {
            var manufacturers = await _manufacturerService.GetAll();
            return View(manufacturers);
        }

        [HttpGet]
        public IActionResult AddManufacturer()
            => PartialView("_AddManufacturer");

        [HttpPost]
        public async Task<IActionResult> AddManufacturer(ManufacturerViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                ModelState.Clear();
                return PartialView("_AddManufacturer", vm);
            }


            var manufacturer = new ManufacturerAddDto()
            {
                Name = vm.Name,
            };
            await _manufacturerService.Post(manufacturer);

            return RedirectToAction("ManufacturerList");
        }

        [HttpGet]
        public async Task<IActionResult> ManufacturerDetails(int id)
        {
            var manufacturers = await _manufacturerService.GetWithProducts(id);
            return View(manufacturers);
        }


        [HttpGet]
        public async Task<IActionResult> EditManufacturer(int id)
        {

            if (id != 0)
            {
                var manufacturer = await _manufacturerService.Get(id);

                var vm = new ManufacturerViewModel
                {
                    Name = manufacturer.Name,
                    Id = manufacturer.Id

                };
                return PartialView("_EditManufacturer", vm);
            }
            return NotFound();


        }

        [HttpPost]
        
        public async Task<IActionResult> EditManufacturer(ManufacturerViewModel vm)
        {
            if (!ModelState.IsValid)
                return PartialView("_EditManufacturer", vm);

            var dto = new ManufacturerEditDto() 
            {
                Id = vm.Id,
                Name = vm.Name,

            };


            await _manufacturerService.Put(dto);


            return RedirectToAction("ManufacturerList");
        }



        [HttpGet]
        [ActionName("DeleteManufacturer")]
        public async Task<IActionResult> ManufacturerDeleteCategory(int? id)
        {
            if (id != null)
            {
                var dto = await _manufacturerService.Get(id.Value);
                if (dto != null)
                {
                    return View(dto);
                }
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteManufacturer(int? id)
        {
            if (id != null)
            {
                var dto = await _manufacturerService.Get(id.Value);
                if (dto != null)
                {
                    await _manufacturerService.Delete(id.Value);
                    return RedirectToAction("ManufacturerList");
                }
            }

            return NotFound();
        }

        #endregion

    }
}
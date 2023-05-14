using Microsoft.AspNetCore.Mvc;
using NukeShop.BLL.Utils.QueryParameters;

namespace NukeShop.WebUI.ModelBinders{


    [ModelBinder(BinderType = typeof(MenuModelBinder))]
    public class MenuSearch
    {
        public MenuSearch()
        {
            OrderBy = "name";
        }
        public int PriceFrom { get; set; } = 0;
        public int PriceTo { get; set; } = 1000000;
        public string? Name { get; set; } = string.Empty;
        public string? Articul { get; set; } = string.Empty;
        public string? CategoryName { get; set; }
        public string? ManufacturerName { get; set; }
        public string? OrderBy { get; set; }
        public int? Page { get; set; }


        public ProductParameters MenuSearchToProductParameters()
        {
            var productParameters = new ProductParameters
            {
                CategoryName = CategoryName ?? "",
                ManufacturerName = ManufacturerName ?? "",
                Articul = !string.IsNullOrEmpty(Articul) ? Articul : "",
                PriceTo = PriceTo,
                PriceFrom = PriceFrom,
                Name = !string.IsNullOrEmpty(Name) ? Name : "",
                OrderBy = OrderBy,
                PageNumber = Page!.Value,
                PageSize = 10,
            };
            return productParameters;
        }
    }
}


using Microsoft.AspNetCore.Mvc.ModelBinding;
using NukeShop.BLL.Utils.QueryParameters;
using System;

namespace NukeShop.WebUI.ModelBinders
{
    public class MenuModelBinder : IModelBinder
    {


        public Task BindModelAsync(ModelBindingContext bindingContext)
        {

            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            var categories = bindingContext.ValueProvider.GetValue("categoryName");
            var manufacturers = bindingContext.ValueProvider.GetValue("manufacturerName");
            var page = bindingContext.ValueProvider.GetValue("page");
            var priceFrom = bindingContext.ValueProvider.GetValue("priceFrom");
            var priceTo = bindingContext.ValueProvider.GetValue("priceTo");

            if ( categories == ValueProviderResult.None &&
                manufacturers == ValueProviderResult.None && 
                page == ValueProviderResult.None &&
                priceTo == ValueProviderResult.None && 
                priceFrom == ValueProviderResult.None)
            {
                bindingContext.Result = ModelBindingResult.Success(new MenuSearch { Page = 1 });
                return Task.CompletedTask;
            }
            var manufacturersList = string.Join('_', manufacturers);
            var categoriesList =  string.Join('_', categories);

            var ms = new MenuSearch();

            ms.CategoryName = categoriesList;
            ms.ManufacturerName = manufacturersList;
            ms.Page = !page.Any() ? 1 : int.Parse(page.ToString());
            ms.PriceFrom = priceFrom.Any() ? int.Parse(priceFrom.ToString()) : ms.PriceFrom;
            ms.PriceTo = priceTo.Any() ? int.Parse(priceTo.ToString()) : ms.PriceTo;
       
         



            bindingContext.Result = ModelBindingResult.Success(ms);
            return Task.CompletedTask;
        }
    }
}
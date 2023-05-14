using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.Extensions.Logging;
using NukeShop.WebUI.Models;
using System.Reflection;

namespace NukeShop.WebUI.ModelBinders
{
    public class ModelBinderProvider : IModelBinderProvider
    {
        private  IModelBinder binder = new MenuModelBinder();

        public IModelBinder? GetBinder(ModelBinderProviderContext context)
        {
            return context.Metadata.ModelType == typeof(MenuModelBinder) ? binder : null;
        }
    }
}
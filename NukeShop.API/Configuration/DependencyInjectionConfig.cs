using NukeShop.DAL;
using NukeShop.DAL.Infrastructure.Interfaces;
using NukeShop.DAL.Infrastructure.Repositories;
using NukeShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NukeShop.API.Configuration
{
    internal static class DependencyInjectionConfig
    {
        public static void ConfigureRepositoryWrapper(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
        }
    }
}

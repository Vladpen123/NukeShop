using NukeShop.DAL.Infrastructure.QueryParameters;
using NukeShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NukeShop.DAL.Infrastructure.Interfaces
{
    public interface IManufacturerRepository : IRepository<Manufacturer>
    {
        Task<PagedList<Manufacturer>> Get(ManufacturerParameters manufacturerParameters);
        Task<Manufacturer> Get(int id);
        Task<Manufacturer> GetWithProducts(int id);
        Task<PagedList<Manufacturer>> GetWithProducts(ManufacturerParameters manufacturerParameters);
        Task<List<Manufacturer>> GetAll();
        Task AddManufacturer(Manufacturer manufacturer);
        void UpdateManufacturer(Manufacturer manufacturer);
        void DeleteManufacturer(Manufacturer manufacturer);
    }
}

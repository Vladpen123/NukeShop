using Microsoft.EntityFrameworkCore;
using NukeShop.DAL.Infrastructure.Interfaces;
using NukeShop.DAL.Infrastructure.QueryParameters;
using NukeShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NukeShop.DAL.Infrastructure.Repositories
{
    public class ManufacturerRepository : Repository<Manufacturer>, IManufacturerRepository
    {
        public ManufacturerRepository(ShopContext db) : base(db)
        {
        }

        public async Task AddManufacturer(Manufacturer manufacturer) => await Create(manufacturer);
        public void DeleteManufacturer(Manufacturer manufacturer) => Remove(manufacturer);
        public void UpdateManufacturer(Manufacturer manufacturer) => Update(manufacturer);
        public async Task<Manufacturer> Get(int id) => await FindById(id);
        public async Task<List<Manufacturer>> GetAll() => await Db.Manufacturers.ToListAsync();
        public async Task<PagedList<Manufacturer>> Get(ManufacturerParameters manufacturerParameters)
        {
            return PagedList<Manufacturer>.ToPagedList(await FindAll(),
                manufacturerParameters.PageNumber,
                manufacturerParameters.PageSize);
        }

        public async Task<PagedList<Manufacturer>> GetWithProducts(ManufacturerParameters manufacturerParameters)
        {

            var manufacturers = await Db.Manufacturers!
                            .Include(p => p.Products)
                            .ThenInclude(c => c.Category).ToListAsync();

            return PagedList<Manufacturer>.ToPagedList(
               manufacturers,
                manufacturerParameters.PageNumber,
                manufacturerParameters.PageSize);
        }

        public async Task<Manufacturer> GetWithProducts(int id) =>
           await Db.Manufacturers!
                    .Include(p => p.Products)
                    .ThenInclude(c => c.Category)
                    .FirstOrDefaultAsync(x => x.Id == id);


    }
}

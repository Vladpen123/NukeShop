using Microsoft.EntityFrameworkCore;
using NukeShop.DAL.Infrastructure.Interfaces;
using NukeShop.DAL.Infrastructure.QueryParameters;
using NukeShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NukeShop.DAL.Infrastructure.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        
        public ProductRepository(ShopContext db) : base(db)
        {
        }

        public async Task AddProduct(Product product)
        {
            await Create(product);
        }

        public void DeleteProduct(Product product)
        {
            Remove(product);
        }


        public async Task<PagedList<Product>> GetAll(ProductParameters productParameters)
        {
            var products = await Db.Products!
                .Where(GetProductPredicateCondition(productParameters))
                .ApplySort(productParameters.OrderBy)
                .Include(p => p.Category)
                .Include(p => p.Manufacturer)  
                .ToListAsync();
                
            return PagedList<Product>.ToPagedList(products, productParameters.PageNumber,
                productParameters.PageSize);
        }

        private static Expression<Func<Product, bool>> GetProductPredicateCondition(ProductParameters productParameters)
        {
            return x =>
               x.Price >= productParameters.PriceFrom &&
               x.Price <= productParameters.PriceTo &&
               (string.IsNullOrEmpty(productParameters.Name) || x.Name.ToLower().Contains(productParameters.Name.ToLower())) &&
               (string.IsNullOrEmpty(productParameters.Articul) || x.Articul.ToLower().Contains(productParameters.Articul.ToLower())) &&
               (string.IsNullOrEmpty(productParameters.ManufacturerName) || productParameters.ManufacturerName.ToLower().Contains(x.Manufacturer.Name.ToLower()))  &&
               (string.IsNullOrEmpty(productParameters.CategoryName) || productParameters.CategoryName.ToLower().Contains(x.Category.Name.ToLower()));
        }

        public async Task<Product> Get(int id)
        {
            return await Db.Products!
            .Include(p => p.Category)
            .Include(p => p.Manufacturer)
            .FirstOrDefaultAsync(x => x.Id == id);

        }

        public void UpdateProduct(Product product)
        {
            Update(product);
        }
      

    }
    
}

using NukeShop.DAL.Infrastructure.QueryParameters;
using NukeShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NukeShop.DAL.Infrastructure.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<PagedList<Product>> GetAll(ProductParameters productParameters);
        Task<Product> Get(int id);
        Task AddProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(Product product);
    }
}

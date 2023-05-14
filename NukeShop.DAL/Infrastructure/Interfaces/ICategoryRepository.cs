using NukeShop.DAL.Infrastructure.QueryParameters;
using NukeShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NukeShop.DAL.Infrastructure.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<PagedList<Category>> Get(CategoryParametrs categoryParametrs);
        Task<List<Category>> GetAll();
        Task<Category> Get(int id);
        Task<Category> GetWithProducts(int id);
        Task<PagedList<Category>> GetWithProducts(CategoryParametrs categoryParametrs);
        Task AddCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(Category category);
    }


}

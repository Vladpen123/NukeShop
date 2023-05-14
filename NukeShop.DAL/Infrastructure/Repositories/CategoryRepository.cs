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
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(ShopContext db) : base(db)
        {
        }
        public async Task AddCategory(Category category) => await Create(category);
        public void UpdateCategory(Category category) => Update(category);
        public void DeleteCategory(Category Category) => Remove(Category);
        public async Task<Category> Get(int id) => await FindById(id);
        public async Task<List<Category>> GetAll() => await Db.Categories.ToListAsync();
        public async Task<PagedList<Category>> Get(CategoryParametrs categoryParametrs)
        {
            return PagedList<Category>.ToPagedList(await FindAll(),
                categoryParametrs.PageNumber,
                categoryParametrs.PageSize);
        }
        public async Task<PagedList<Category>> GetWithProducts(CategoryParametrs categoryParametrs)
        {
            var categories = await Db.Categories!
                .Include(p => p.Products)
                .ThenInclude(m => m.Manufacturer).ToListAsync();

            return PagedList<Category>.ToPagedList(
                categories,
                    categoryParametrs.PageNumber,
                    categoryParametrs.PageSize);
        }
        public async Task<Category> GetWithProducts(int id)
        {
            return await Db.Categories!
            .Include(p => p.Products)
            .ThenInclude(m => m.Manufacturer)
            .FirstOrDefaultAsync(x => x.Id == id);

        }

    }
}

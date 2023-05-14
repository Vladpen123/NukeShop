using Microsoft.EntityFrameworkCore;
using NukeShop.DAL.Infrastructure.Interfaces;
using NukeShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NukeShop.DAL.Infrastructure.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : Entity
    {
        protected ShopContext Db { get; set; }


        public Repository(ShopContext db)
        {
            Db = db;

        }
        public async Task<List<T>> FindByCondition(Expression<Func<T, bool>> predicate) =>
            await Db.Set<T>().Where(predicate).AsNoTracking().ToListAsync();

        public async Task<List<T>> FindAll() =>
            await Db.Set<T>().AsNoTracking().ToListAsync();

        public async Task Create(T entity)
        {
            
            await Db.Set<T>().AddAsync(entity);

        }

        public void Update(T entity)
        {
            Db.Set<T>().Update(entity);

        }

        public async void Remove(T entity)
        {
            Db.Set<T>().Remove(entity);
        }

        public async Task<T> FindById(int id)
        {
            return await Db.Set<T>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}

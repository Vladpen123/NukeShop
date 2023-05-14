using NukeShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NukeShop.DAL.Infrastructure.Interfaces
{
    public interface IRepository<T> where T : Entity 
    {

        Task<List<T>> FindByCondition(Expression<Func<T, bool>> predicate);
        Task<T> FindById(int id);
        Task<List<T>> FindAll();
        Task Create(T entity);
        void Update(T entity);
        void Remove(T entity);
  
    }
}

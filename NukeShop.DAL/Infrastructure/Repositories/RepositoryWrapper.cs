using NukeShop.DAL.Infrastructure.Interfaces;
using NukeShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NukeShop.DAL.Infrastructure.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {

        private ShopContext _shopContext;

        private IProductRepository _productRepository;
        private ICategoryRepository _categoryRepository;
        private IManufacturerRepository _manufacturerRepository;

   

        public IProductRepository Product { 
            get
            { 
                if(_productRepository == null)
                {
                    _productRepository = new ProductRepository(_shopContext);
                }
                return _productRepository;
            } 
        }
        public ICategoryRepository Category
        {
            get
            {
                if (_categoryRepository == null)
                {
                    _categoryRepository = new CategoryRepository(_shopContext);
                }
                return _categoryRepository;
            }
        }
        public IManufacturerRepository Manufacturer
        {
            get
            {
                if (_manufacturerRepository == null)
                {
                    _manufacturerRepository = new ManufacturerRepository(_shopContext);
                }
                return _manufacturerRepository;
            }
        }

        public RepositoryWrapper(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }


        public void Save()
        {
            _shopContext.SaveChanges();
        }
    }
}

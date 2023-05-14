using NukeShop.BLL.DTOs;
using NukeShop.BLL.DTOs.CategoryDtos;
using NukeShop.BLL.DTOs.ProductDtos;
using NukeShop.BLL.Utils.QueryParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NukeShop.BLL.Services
{
    public interface IProductService 
    {
        Task Delete(int id);
        Task<ProductDto> Get(int id);
        Task<PagingResponse<ProductDto>> Search(ProductParameters? productParameters);
        Task Post(ProductAddDto dto);
        Task Put(ProductEditDto dto);
    }
}

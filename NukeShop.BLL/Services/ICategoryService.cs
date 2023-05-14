using NukeShop.BLL.DTOs;
using NukeShop.BLL.DTOs.CategoryDtos;
using NukeShop.BLL.DTOs.ManufacturerDtos;
using NukeShop.BLL.Utils.QueryParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NukeShop.BLL.Services
{
    public interface ICategoryService
    {
        Task Delete(int id);
        Task<CategoryDto> Get(int id);
        Task<List<CategoryDto>> GetAll();
        Task<PagingResponse<CategoryDto>> Get(CategoryParametrs categoryParametrs);
        Task Post(CategoryAddDto dto);
        Task Put(CategoryEditDto dto);
        Task<CategoryDto> GetWithProducts(int id);
        Task<PagingResponse<CategoryDto>> GetWithProducts(CategoryParametrs categoryParametrs);

    }
}

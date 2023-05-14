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
    public interface IManufacturerService
    {
        Task Delete(int id);
        Task<ManufacturerDto> Get(int id);
        Task<List<ManufacturerDto>> GetAll();
        Task<PagingResponse<ManufacturerDto>> Get(ManufacturerParameters manufacturerParameters);
        Task Post(ManufacturerAddDto dto);
        Task Put(ManufacturerEditDto dto);
        Task<ManufacturerDto> GetWithProducts(int id);
        Task<PagingResponse<ManufacturerDto>> GetWithProducts(ManufacturerParameters manufacturerParameters);
    }
}

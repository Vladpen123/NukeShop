using Microsoft.AspNetCore.WebUtilities;
using NukeShop.BLL.DTOs;
using NukeShop.BLL.DTOs.CategoryDtos;
using NukeShop.BLL.DTOs.ManufacturerDtos;
using NukeShop.BLL.Utils.QueryParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NukeShop.BLL.Services
{
    public class ManufacturerService : IManufacturerService
    {
        private readonly HttpClient client;
        
        private readonly JsonSerializerOptions _options;
        public ManufacturerService(HttpClient client)
        {
            this.client = client;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }
        public async Task<ManufacturerDto> GetWithProducts(int id)
        {
            var result = await client.GetFromJsonAsync<ManufacturerDto>($"Manufacturer/WithProducts/{id}");
            return result;
        }

        public async Task<PagingResponse<ManufacturerDto>> GetWithProducts(ManufacturerParameters manufacturerParameters)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["page"] = manufacturerParameters.PageNumber.ToString()
            };
            var response = await client.GetAsync(QueryHelpers.AddQueryString("Manufacturer/WithProducts", queryStringParam));
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var pagingResponse = new PagingResponse<ManufacturerDto>()
            {
                Items = JsonSerializer.Deserialize<List<ManufacturerDto>>(content, _options)!,
                MetaData = JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-Pagination").First(), _options)!
            };

            return pagingResponse;
        }

        public async Task Delete(int id)
        {
            await client.DeleteAsync($"Manufacturer/{id}");
        }

        public async Task<List<ManufacturerDto>> GetAll()
        {
            return await client.GetFromJsonAsync<List<ManufacturerDto>>("Manufacturer/All");
        }

        public async Task<ManufacturerDto> Get(int id)
        {
            return await client.GetFromJsonAsync<ManufacturerDto>($"Manufacturer/{id}");
        }


        public async Task<PagingResponse<ManufacturerDto>> Get(ManufacturerParameters manufacturerParameters)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["page"] = manufacturerParameters.PageNumber.ToString()
            };
            var response = await client.GetAsync(QueryHelpers.AddQueryString("Manufacturer/", queryStringParam));
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var pagingResponse = new PagingResponse<ManufacturerDto>()
            {
                Items = JsonSerializer.Deserialize<List<ManufacturerDto>>(content, _options)!,
                MetaData = JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-Pagination").First(), _options)!
            };

            return pagingResponse;
        }

        public async Task Post(ManufacturerAddDto dto)
        {
            await client.PostAsJsonAsync($"Manufacturer/", dto);
        }

        public async Task Put(ManufacturerEditDto dto)
        {
            var response = await client.PutAsJsonAsync($"Manufacturer/{dto.Id}", dto);
            response.EnsureSuccessStatusCode();
        }
    }
}

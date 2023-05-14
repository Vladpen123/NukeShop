using Microsoft.AspNetCore.WebUtilities;
using NukeShop.BLL.DTOs;
using NukeShop.BLL.DTOs.CategoryDtos;
using NukeShop.BLL.DTOs.ProductDtos;
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
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient client;

        private readonly JsonSerializerOptions _options;
        public CategoryService(HttpClient client)
        {
            this.client = client;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task Delete(int id)
        {
            await client.DeleteAsync($"Category/{id}");
        }

        public async Task<CategoryDto> Get(int id)
        {
            return await client.GetFromJsonAsync<CategoryDto>($"Category/{id}");
        }
        public async Task<CategoryDto> GetWithProducts(int id)
        {
            var result = await client.GetFromJsonAsync<CategoryDto>($"Category/WithProducts/{id}");
            return result;
        }

        public async Task<PagingResponse<CategoryDto>> GetWithProducts(CategoryParametrs categoryParametrs)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["page"] = categoryParametrs.PageNumber.ToString()
            };
            var response = await client.GetAsync(QueryHelpers.AddQueryString("Category/WithProducts", queryStringParam));
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var pagingResponse = new PagingResponse<CategoryDto>()
            {
                Items = JsonSerializer.Deserialize<List<CategoryDto>>(content, _options)!,
                MetaData = JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-Pagination").First(), _options)!
            };

            return pagingResponse;
            
        }

        public async Task<PagingResponse<CategoryDto>> Get(CategoryParametrs? categoryParametrs)
        {
            categoryParametrs ??= new CategoryParametrs();
            var queryStringParam = new Dictionary<string, string>
            {
                ["page"] = categoryParametrs.PageNumber.ToString()
            };
            var response = await client.GetAsync(QueryHelpers.AddQueryString("Category/", queryStringParam));
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var pagingResponse = new PagingResponse<CategoryDto>()
            {
                Items = JsonSerializer.Deserialize<List<CategoryDto>>(content, _options)!,
                MetaData = JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-Pagination").First(), _options)!
            };

            return pagingResponse;
        }

        public async Task<List<CategoryDto>> GetAll()
        {     
            return await client.GetFromJsonAsync<List<CategoryDto>>("Category/All");
        }

        public async Task Post(CategoryAddDto dto)
        {
            var response = await client.PostAsJsonAsync("Category/", dto);
            response.EnsureSuccessStatusCode();
        }

        public async Task Put(CategoryEditDto dto)
        {
            var response = await client.PutAsJsonAsync($"Category/{dto.Id}", dto);
            response.EnsureSuccessStatusCode();
        }
    }
}

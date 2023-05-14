using AutoMapper;
using Microsoft.AspNetCore.WebUtilities;
using NukeShop.BLL.DTOs;
using NukeShop.BLL.DTOs.ProductDtos;
using NukeShop.BLL.Utils.QueryParameters;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Web;

namespace NukeShop.BLL.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient client;
        private readonly JsonSerializerOptions _options;
        public ProductService(HttpClient client)
        {
            this.client = client;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        }



        public async Task<PagingResponse<ProductDto>> Search(ProductParameters? productParameters)
        {

            var queryStringParam = new Dictionary<string, string>
            {
                ["pageSize"] = productParameters.PageSize.ToString(),
                ["pageNumber"] = productParameters.PageNumber.ToString(),
                ["PriceFrom"] = productParameters.PriceFrom.ToString(),
                ["PriceTo"] = productParameters.PriceTo.ToString(),
                ["Name"] = productParameters.Name.ToString(),
                ["Articul"] = productParameters.Articul.ToString() ?? string.Empty,
                ["CategoryName"] = productParameters.CategoryName.ToString(),
                ["ManufacturerName"] = productParameters.ManufacturerName.ToString(),
                ["OrderBy"] = productParameters.OrderBy.ToString(),

            };
            var response = await client.GetAsync(QueryHelpers.AddQueryString("Product/Search", queryStringParam));
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var pagingResponse = new PagingResponse<ProductDto>()
            {
                Items = JsonSerializer.Deserialize<List<ProductDto>>(content, _options)!,
                MetaData = JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-Pagination").First(), _options)!
            };

            return pagingResponse;
        }


        public async Task<ProductDto> Get(int id)
        {
            ProductDto? product = await client.GetFromJsonAsync<ProductDto>($"Product/{id}");
            return product;
        }

        public async Task Post(ProductAddDto dto)
        {
            var response = await client.PostAsJsonAsync("Product/", dto);
            response.EnsureSuccessStatusCode();

        }

        public async Task Put(ProductEditDto dto)
        {
            var response = await client.PutAsJsonAsync($"Product/{dto.Id}", dto);
            response.EnsureSuccessStatusCode();
        }

        public async Task Delete(int id)
        {
            await client.DeleteAsync($"Product/{id}");   
        }

 
    }
}

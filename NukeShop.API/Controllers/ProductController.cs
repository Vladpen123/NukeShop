using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NukeShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using NukeShop.DAL.Infrastructure.Interfaces;
using NukeShop.API.Models.DTOs;
using NukeShop.API.Models.Logger;
using NukeShop.DAL.Infrastructure.Repositories;
using NukeShop.DAL.Infrastructure.QueryParameters;
using Newtonsoft.Json;
using NukeShop.DAL.Infrastructure;

namespace NukeShop.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryWrapper _repo;
        private readonly IMapper _mapper;
        public ProductController(ILoggerManager logger, IRepositoryWrapper repo, IMapper mapper)
        {
            _logger = logger;
            _repo = repo;
            _mapper = mapper;
        }




        [HttpGet]
        [Route("Search/")]
        public async Task<IActionResult> Search([FromQuery] ProductParameters productParameters)
        {
            try
            {
                if (productParameters.PriceFrom < decimal.Zero ||
                    productParameters.PriceTo < productParameters.PriceFrom)
                {
                    return BadRequest("The upper bound (PriceTo) of price cant be lower than lower bound (PriceFrom)");
                }

                var products = await _repo.Product.GetAll(productParameters);


                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(products.MetaData));

                _logger.LogInfo($"Returned all products from database.");
                var result = _mapper.Map<IEnumerable<ProductResultDto>>(products);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllProducts action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var product = await _repo.Product.Get(id);
                if (product == null)
                {
                    _logger.LogInfo($"Product with id: {id} hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned product with id: {id}");
                    var result = _mapper.Map<ProductResultDto>(product);
                    return Ok(result);
                }


            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside Get action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductAddDto product)
        {
            try
            {
                if (product == null)
                {
                    _logger.LogError("Product object sent from client is null");
                    return BadRequest("Product object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid product object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var productEntity = _mapper.Map<Product>(product);

                await _repo.Product.AddProduct(productEntity);
                _repo.Save();

                var createdProduct = _mapper.Map<ProductResultDto>(productEntity);

                return CreatedAtAction("Get", new { id = createdProduct.Id }, createdProduct);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateProduct action: {ex.Message}");
                return StatusCode(500, "Internal server error");

            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductEditDto product)
        {
            try
            {
                if (product == null)
                {
                    _logger.LogError("Product object sent from client is null.");
                    return BadRequest("Product object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid product object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var productEntity = await _repo.Product.Get(id);

                if (productEntity == null)
                {
                    _logger.LogError($"Product with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _mapper.Map(product, productEntity);

                _repo.Product.UpdateProduct(productEntity);
                _repo.Save();

                return NoContent();

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateProduct action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                var product = await _repo.Product.Get(id);
                if (product == null)
                {
                    _logger.LogError($"Product with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                _repo.Product.DeleteProduct(product); 
                _repo.Save();
            
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteProduct action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

    }
}
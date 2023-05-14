using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NukeShop.API.Models.DTOs;
using NukeShop.API.Models.Logger;
using NukeShop.DAL.Infrastructure;
using NukeShop.DAL.Infrastructure.Interfaces;
using NukeShop.DAL.Infrastructure.QueryParameters;
using NukeShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NukeShop.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryWrapper _repo;
        private readonly IMapper _mapper;
        public CategoryController(ILoggerManager logger, IRepositoryWrapper repo, IMapper mapper)
        {
            _logger = logger;
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("WithProducts")]
        public async Task<IActionResult> GetCategoriesWithProducts([FromQuery] CategoryParametrs categoryParameters)
        {
            try
            {
                var categories = await _repo.Category.GetWithProducts(categoryParameters);

           
                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(categories.MetaData));

                _logger.LogInfo($"Returned all categories from database.");
               
                var result = _mapper.Map<List<CategoryResultDto>>(categories);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllCategorys action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories([FromQuery] CategoryParametrs categoryParameters)
        {
            try
            {
                var categories = await _repo.Category.Get(categoryParameters);

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(categories.MetaData));

                _logger.LogInfo($"Returned all categories from database.");

                var result = _mapper.Map<List<CategoryResultDto>>(categories);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllCategorys action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        [Route("All/")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var manufacturers = await _repo.Category.GetAll();

                _logger.LogInfo($"Returned all categories from database.");

                var result = _mapper.Map<List<CategoryResultDto>>(manufacturers);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAll action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        [Route("WithProducts/{id:int}")]
        public async Task<IActionResult> GetWithProducts(int id)
        {
            try
            {
                var category = await _repo.Category.GetWithProducts(id);
                if (category == null)
                {
                    _logger.LogInfo($"Category with id: {id} hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned category with id: {id}");
                    var result = _mapper.Map<CategoryResultDto>(category);
                    return Ok(result);
                }


            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside Get action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var category = await _repo.Category.Get(id);
                if (category == null)
                {
                    _logger.LogInfo($"Category with id: {id} hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned category with id: {id}");
                    var result = _mapper.Map<CategoryResultDto>(category);
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
        public async Task<IActionResult> CreateCategory([FromBody] CategoryAddDto category)
        {
            try
            {
                if (category == null)
                {
                    _logger.LogError("Category object sent from client is null");
                    return BadRequest("Category object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid category object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var categoryEntity = _mapper.Map<Category>(category);

                await _repo.Category.AddCategory(categoryEntity);
                _repo.Save();

                var createdCategory = _mapper.Map<Category>(categoryEntity);

                return CreatedAtAction("Get", new { id = createdCategory.Id }, createdCategory);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateCategory action: {ex.Message}");
                return StatusCode(500, "Internal server error");

            }
        }


        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryEditDto category)
        {
            try
            {
                if (category == null)
                {
                    _logger.LogError("Category object sent from client is null.");
                    return BadRequest("Category object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid category object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var categoryEntity = await _repo.Category.Get(id);

                if (categoryEntity == null)
                {
                    _logger.LogError($"Category with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                _mapper.Map(category, categoryEntity);

                _repo.Category.UpdateCategory(categoryEntity);
                _repo.Save();

                return NoContent();

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateCategory action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                var category = await _repo.Category.Get(id);
                if (category == null)
                {
                    _logger.LogError($"Category with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                _repo.Category.DeleteCategory(category);
                _repo.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteCategory action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

    }
}
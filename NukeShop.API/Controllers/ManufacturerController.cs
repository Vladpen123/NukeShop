using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NukeShop.API.Models.DTOs;
using NukeShop.API.Models.Logger;
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
    public class ManufacturerController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryWrapper _repo;
        private readonly IMapper _mapper;
        public ManufacturerController(ILoggerManager logger, IRepositoryWrapper repo, IMapper mapper)
        {
            _logger = logger;
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("WithProducts/")]
        public async Task<IActionResult> GetManufacturersWithProducts([FromQuery] ManufacturerParameters manufacturerParameters)
        {
            try
            {
                var manufacturers = await _repo.Manufacturer.GetWithProducts(manufacturerParameters);


                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(manufacturers.MetaData));

                _logger.LogInfo($"Returned all manufacturers from database.");

                var result = _mapper.Map<List<ManufacturerResultDto>>(manufacturers);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllManufacturers action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetManufacturers([FromQuery] ManufacturerParameters manufacturerParameters)
        {
            try
            {
                var manufacturers = await _repo.Manufacturer.Get(manufacturerParameters);

 
                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(manufacturers.MetaData));

                _logger.LogInfo($"Returned all manufacturers from database.");

                var result = _mapper.Map<List<ManufacturerResultDto>>(manufacturers);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllManufacturers action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        [Route("All/")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var manufacturers = await _repo.Manufacturer.GetAll();

                _logger.LogInfo($"Returned all manufacturers from database.");

                var result = _mapper.Map<List<ManufacturerResultDto>>(manufacturers);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllManufacturers action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpGet]
        [Route("WithProducts/{id:int}")]
        public async Task<IActionResult> GetWithProducts(int id)
        {
            try
            {
                var manufacturer = await _repo.Manufacturer.GetWithProducts(id);
                if (manufacturer == null)
                {
                    _logger.LogInfo($"Manufacturer with id: {id} hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned manufacturer with id: {id}");
                    var result = _mapper.Map<ManufacturerResultDto>(manufacturer);
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
                var manufacturer = await _repo.Manufacturer.Get(id);
                if (manufacturer == null)
                {
                    _logger.LogInfo($"Manufacturer with id: {id} hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned manufacturer with id: {id}");
                    var result = _mapper.Map<ManufacturerResultDto>(manufacturer);
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
        public async Task<IActionResult> CreateManufacturer([FromBody] ManufacturerAddDto manufacturer)
        {
            try
            {
                if (manufacturer == null)
                {
                    _logger.LogError("Manufacturer object sent from client is null");
                    return BadRequest("Manufacturer object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid manufacturer object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var manufacturerEntity = _mapper.Map<Manufacturer>(manufacturer);

                await _repo.Manufacturer.AddManufacturer(manufacturerEntity);
                _repo.Save();
                
                var createdManufacturer = _mapper.Map<Manufacturer>(manufacturerEntity);

                return CreatedAtRoute("Manufacturer", new { id = createdManufacturer }, createdManufacturer);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateManufacturer action: {ex.Message}");
                return StatusCode(500, "Internal server error");

            }
        }


        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateManufacturer(int id, [FromBody] ManufacturerEditDto manufacturer)
        {
            try
            {
                if (manufacturer == null)
                {
                    _logger.LogError("Manufacturer object sent from client is null.");
                    return BadRequest("Manufacturer object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid manufacturer object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var manufacturerEntity = await _repo.Manufacturer.Get(id);

                if (manufacturerEntity == null)
                {
                    _logger.LogError($"Manufacturer with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _mapper.Map(manufacturer, manufacturerEntity);

                _repo.Manufacturer.UpdateManufacturer(manufacturerEntity);
                _repo.Save();

                return NoContent();

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateManufacturer action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteManufacturer(int id)
        {
            try
            {
                var manufacturer = await _repo.Manufacturer.Get(id);
                if (manufacturer == null)
                {
                    _logger.LogError($"Manufacturer with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                _repo.Manufacturer.DeleteManufacturer(manufacturer);
                _repo.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteManufacturer action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

    }
}
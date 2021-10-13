using API.Data;
using API.Entities;
using API.Interfaces.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class CarPropertiesController : AppController
    {
        private readonly ICarPropertiesRepository _carPropertiesRepository;

        public CarPropertiesController(ICarPropertiesRepository carPropertiesRepository)
        {
            _carPropertiesRepository = carPropertiesRepository;
        }

        [HttpGet("getBrands")]
        public async Task<ActionResult<List<Brand>>> GetBrands()
        {
            var data = await _carPropertiesRepository.GetBrandsToList();

            return Ok(data);
        }

        [HttpGet("getModels/{id}")]
        public async Task<ActionResult<List<Brand>>> GetModels(int id)
        {
            var data = await _carPropertiesRepository.GetModelsToList(id);

            return Ok(data);
        }
    }
}

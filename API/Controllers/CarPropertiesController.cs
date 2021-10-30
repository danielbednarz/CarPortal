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
        public async Task<ActionResult<List<Model>>> GetModels(int id)
        {
            var data = await _carPropertiesRepository.GetModelsToList(id);

            return Ok(data);
        }

        [HttpGet("getEnginesForModel/{modelId}")]
        public async Task<ActionResult<List<EnginesForModel>>> GetEnginesForModel(int modelId)
        {
            var enginesForModel = await _carPropertiesRepository.GetEnginesForModel(modelId);

            var engineIds = new List<int>();

            foreach(var item in enginesForModel)
            {
                engineIds.Add(item.EngineId);
            }

            var data = await _carPropertiesRepository.GetEngines(engineIds);

            return Ok(data);
        }

    }
}

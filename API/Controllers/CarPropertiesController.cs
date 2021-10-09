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
        private readonly IBrandRepository _brandRepository;

        public CarPropertiesController(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }

        [HttpGet("getBrands")]
        public async Task<ActionResult<List<Brand>>> GetBrands()
        {
            var data = await _brandRepository.GetBrandsToList();

            return Ok(data);
        }
    }
}

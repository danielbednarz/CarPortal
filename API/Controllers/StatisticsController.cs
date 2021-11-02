using API.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        //[HttpGet]
        //public async Task<ActionResult<List<FuelReport>>> GetFuelReport()
        //{

        //}

        // POST api/<StatisticsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<StatisticsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<StatisticsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

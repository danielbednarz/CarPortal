using API.Data;
using API.Entities;
using API.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class StatisticsController : AppController
    {
        private readonly MainDatabaseContext _context;
        private readonly IStatisticsRepository _statisticsRepository;

        public StatisticsController(MainDatabaseContext context, IStatisticsRepository statisticsRepository)
        {
            _context = context;
            _statisticsRepository = statisticsRepository;
        }

        [HttpGet("getFuelReport/{userId}")]
        public async Task<ActionResult<List<FuelReport>>> GetFuelReport(int userId)
        {
            var data = await _statisticsRepository.GetFuelReportToList(userId);

            return Ok(data);
        }

        [HttpPost("add-fuel-report")]
        public async Task<ActionResult<FuelReport>> AddFuelReport(FuelReport fuelReport)
        {
            _context.FuelReports.Add(fuelReport);

            await _context.SaveChangesAsync();

            return Ok();
        }

    }
}

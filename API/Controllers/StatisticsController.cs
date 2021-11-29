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

        [HttpGet("getFuelReportView/{userId}")]
        public async Task<ActionResult<List<FuelReport>>> GetFuelReportView(int userId)
        {
            var data = await _statisticsRepository.GetFuelReportViewToList(userId);

            return Ok(data);
        }

        [HttpGet("getAverageConsumption/{userId}")]
        public async Task<ActionResult> GetAverageConsumption(int userId)
        {
            var data = await _statisticsRepository.GetAverageConsumption(userId);

            return Ok(data);
        }

        [HttpPost("add-fuel-report")]
        public async Task<ActionResult<FuelReport>> AddFuelReport(FuelReport fuelReport)
        {
            _context.FuelReports.Add(fuelReport);

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("getRepairReport/{userId}")]
        public async Task<ActionResult<List<RepairReport>>> GetRepairReport(int userId)
        {
            var data = await _statisticsRepository.GetRepairReport(userId);

            return Ok(data);
        }

        [HttpGet("getRepairReportView/{userId}")]
        public async Task<ActionResult<List<FuelReport>>> GetRepairReportView(int userId)
        {
            var data = await _statisticsRepository.GetRepairReportView(userId);

            return Ok(data);
        }

        [HttpPost("addRepairReport")]
        public async Task<ActionResult> AddRepairReport(RepairReport repairReport)
        {
            _context.RepairReports.Add(repairReport);

            await _context.SaveChangesAsync();

            return Ok();
        }

    }
}

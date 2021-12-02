using API.Data;
using API.Entities;
using API.Entities.Views;
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
        private readonly IUserRepository _userRepository;

        public StatisticsController(MainDatabaseContext context, IStatisticsRepository statisticsRepository, IUserRepository userRepository)
        {
            _context = context;
            _statisticsRepository = statisticsRepository;
            _userRepository = userRepository;
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

        [HttpDelete("deleteFuelReport/{fuelReportId}")]
        public async Task<ActionResult> DeleteFuelReport(string fuelReportId)
        {
            var fuelReport = _context.FuelReports.FirstOrDefault(x => x.Id == Guid.Parse(fuelReportId));

            if (fuelReport == null)
            {
                return NotFound();
            }

            _context.FuelReports.Remove(fuelReport);
            await _userRepository.SaveAllAsync();

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

        [HttpDelete("deleteRepairReport/{repairReportId}")]
        public async Task<ActionResult> DeleteRepairReport(string repairReportId)
        {
            var repairReport = _context.RepairReports.FirstOrDefault(x => x.Id == Guid.Parse(repairReportId));

            if (repairReport == null)
            {
                return NotFound();
            }

            _context.RepairReports.Remove(repairReport);
            await _userRepository.SaveAllAsync();

            return Ok();
        }

        [HttpGet("getTotalCostsReportView/{userId}")]
        public async Task<ActionResult<List<FuelReport>>> GetTotalCostsReportView(int userId)
        {
            var data = await _statisticsRepository.GetTotalCostsReportView(userId);

            return Ok(data);
        }

        [HttpGet("getTotalRepairFuelCostsReportView/{userId}")]
        public async Task<ActionResult<List<TotalRepairFuelCostsReportView>>> GetTotalRepairFuelCostsReportView(int userId)
        {
            var data = await _statisticsRepository.GetTotalRepairFuelCostsReportView(userId);

            return Ok(data);
        }

    }
}

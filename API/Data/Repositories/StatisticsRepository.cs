using API.Entities;
using API.Entities.Views;
using API.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data.Repositories
{
    public class StatisticsRepository : IStatisticsRepository
    {
        private readonly MainDatabaseContext _context;

        public StatisticsRepository(MainDatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<FuelReport>> GetFuelReportToList(int userId)
        {
            return await _context.FuelReports.Where(x => x.UserId == userId).ToListAsync();
        }

        public async Task<List<FuelReportView>> GetFuelReportViewToList(int userId)
        {
            return await _context.FuelReportView.Where(x => x.UserId == userId).ToListAsync();
        }

        public async Task<List<FuelReportView>> GetAverageConsumption(int userId)
        {
            return await _context.FuelReportView.FromSqlInterpolated(@$"SELECT
												'' as [Month],
												CAST(ISNULL(((SUM(fuelReport.FuelAmount) * 100) / SUM(fuelReport.TraveledDistance)), 0) as decimal(18,2)) as [AverageConsumption],
                                                CAST(ISNULL((SUM(fuelReport.Cost) * 100) / SUM(fuelReport.TraveledDistance), 0) as decimal(18,2)) as [AverageCost],
												{userId} as [UserId]
											FROM FuelReports fuelReport
											WHERE UserId = {userId}").ToListAsync(); 

		}

        public async Task<List<RepairReport>> GetRepairReport(int userId)
        {
            return await _context.RepairReports.Where(x => x.UserId == userId).ToListAsync();
        }

        public async Task<List<RepairReportView>> GetRepairReportView(int userId)
        {
            return await _context.RepairReportView.Where(x => x.UserId == userId).ToListAsync();
        }
        public async Task<List<TotalCostsReportView>> GetTotalCostsReportView(int userId)
        {
            return await _context.TotalCostsReportView.Where(x => x.UserId == userId).ToListAsync();
        }
        public async Task<List<TotalRepairFuelCostsReportView>> GetTotalRepairFuelCostsReportView(int userId)
        {

            return await _context.TotalRepairFuelCostsReportView.FromSqlInterpolated(@$"SELECT 'Wydatki na paliwo' as [Text]
                                                                  , ISNULL(SUM(fuel.Cost), 0) as [Value]
                                                            FROM FuelReports fuel
                                                            WHERE fuel.UserId = {userId}
                                                            UNION
                                                            SELECT 'Wydatki na naprawy i koszty eksploatacyjne' as [Text]
                                                                  , ISNULL(SUM(repair.Cost), 0) as [Value]
                                                            FROM RepairReports repair
                                                            WHERE repair.UserId = {userId}").ToListAsync();
        }
    }
}

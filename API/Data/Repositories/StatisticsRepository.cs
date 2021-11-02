using API.Entities;
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
    }
}

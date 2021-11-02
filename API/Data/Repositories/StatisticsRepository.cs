using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data.Repositories
{
    public class StatisticsRepository
    {
        private readonly MainDatabaseContext _context;

        public StatisticsRepository(MainDatabaseContext context)
        {
            _context = context;
        }
    }
}

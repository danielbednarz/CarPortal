using API.DTOs;
using API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Interfaces.Repositories
{
    public interface IStatisticsRepository
    {
        Task<List<FuelReport>> GetFuelReportToList(int userId);
    }
}

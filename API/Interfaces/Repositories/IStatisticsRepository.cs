using API.Entities;
using API.Entities.Views;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Interfaces.Repositories
{
    public interface IStatisticsRepository
    {
        Task<List<FuelReport>> GetFuelReportToList(int userId);
        Task<List<FuelReportView>> GetFuelReportViewToList(int userId);
        Task<List<FuelReportView>> GetAverageConsumption(int userId);
        Task<List<RepairReport>> GetRepairReport(int userId);
        Task<List<RepairReportView>> GetRepairReportView(int userId);
    }
}

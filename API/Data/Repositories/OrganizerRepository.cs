using API.Interfaces.Repositories;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data.Repositories
{
    public class OrganizerRepository : IOrganizerRepository
    {
        private readonly MainDatabaseContext _context;

        public OrganizerRepository(MainDatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<CarInsurance>> GetCarInsurance(int userId)
        {
            return await _context.CarInsurances.Where(u => u.UserId == userId).OrderByDescending(x => x.ExpirationDate).ToListAsync();
        }

        public async Task<int> GetCarInsuranceRemainingDays(int userId)
        {
            var query = await _context.CarInsuranceRemainingDays.FromSqlInterpolated($@"SELECT TOP(1) ISNULL(DATEDIFF(DAY, GETDATE(), carInsurance.ExpirationDate), -999999) as remainingDays
                                                                  FROM [CarInsurances] carInsurance
                                                                  WHERE carInsurance.UserId = {userId}
                                                                  ORDER BY carInsurance.ExpirationDate DESC").ToListAsync();

            if(query.Count == 0)
            {
                return -99999;
            }

            var remainingDays = query[0].RemainingDays;

            return remainingDays;
        }

        public async Task<List<PeriodicInspection>> GetPeriodicInspections(int userId)
        {
            return await _context.PeriodicInspections.Where(u => u.UserId == userId).OrderByDescending(x => x.InspectionDate).ToListAsync();
        }

        public async Task<int> GetPeriodicInspectionRemainingDays(int userId)
        {
            var query = await _context.CarInsuranceRemainingDays.FromSqlInterpolated(@$"SELECT TOP(1) DATEDIFF(DAY, GETDATE(), periodicInspection.InspectionDate) as remainingDays
                                                                  FROM PeriodicInspections periodicInspection
                                                                  WHERE periodicInspection.UserId = {userId} AND periodicInspection.isPositive = 1
                                                                  ORDER BY periodicInspection.InspectionDate DESC").ToListAsync();

            if(query.Count == 0)
            {
                return -99999;
            }

            var remainingDays = query[0].RemainingDays;

            return remainingDays;
        }
    }
}

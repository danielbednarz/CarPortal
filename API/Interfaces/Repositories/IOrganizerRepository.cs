using API.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Interfaces.Repositories
{
    public interface IOrganizerRepository
    {
        public Task<List<CarInsurance>> GetCarInsurance(int userId);
        public Task<int> GetCarInsuranceRemainingDays(int userId);
    }
}

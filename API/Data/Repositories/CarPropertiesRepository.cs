using API.Entities;
using API.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data.Repositories
{
    public class CarPropertiesRepository : ICarPropertiesRepository
    {
        private readonly MainDatabaseContext _context;

        public CarPropertiesRepository(MainDatabaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Brand>> GetBrandsToList()
        {
            return await _context.Brands.ToListAsync();
        }

        public async Task<IEnumerable<Model>> GetModelsToList(int id)
        {
            return await _context.Models.Where(x => x.BrandId == id).ToListAsync();
        }
    }
}

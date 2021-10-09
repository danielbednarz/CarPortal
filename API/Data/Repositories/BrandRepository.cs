using API.Entities;
using API.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Data.Repositories
{
    public class BrandRepository : IBrandRepository
    {
        private readonly MainDatabaseContext _context;

        public BrandRepository(MainDatabaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Brand>> GetBrandsToList()
        {
            return await _context.Brands.ToListAsync();
        }
    }
}

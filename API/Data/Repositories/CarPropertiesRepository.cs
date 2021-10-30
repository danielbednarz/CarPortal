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
            return await _context.Models.Where(x => x.BrandId == id).OrderBy(y => y.Name).ToListAsync();
        }

        public async Task<IEnumerable<EnginesForModel>> GetEnginesForModel(int modelId)
        {
            return await _context.EnginesForModels.Include(x => x.Engine).Include(y => y.Model).Where(i => i.ModelId == modelId).ToListAsync();
        }

        public async Task<IEnumerable<Engine>> GetEngines(List<int> engineIds)
        {
            var engineList = new List<Engine>();

            foreach(var item in engineIds)
            {
                engineList.Add(_context.Engines.FirstOrDefault(x => x.Id == item));
            }

            return engineList.OrderBy(x => x.EngineCapacity);
        }
    }
}

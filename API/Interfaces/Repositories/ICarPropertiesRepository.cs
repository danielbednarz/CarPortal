using API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Interfaces.Repositories
{
    public interface ICarPropertiesRepository
    {
        Task<IEnumerable<Brand>> GetBrandsToList();
        Task<IEnumerable<Model>> GetModelsToList(int id);
        Task<IEnumerable<EnginesForModel>> GetEnginesForModel(int modelId);
        Task<IEnumerable<Engine>> GetEngines(List<int> engineIds);
    }
}

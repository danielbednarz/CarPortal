using API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Interfaces.Repositories
{
    public interface IBrandRepository
    {
        Task<IEnumerable<Brand>> GetBrandsToList();
    }
}

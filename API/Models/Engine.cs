using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class Engine
    {
        public int Id { get; set; }
        public string EngineCapacity { get; set; }
        public int EnginePower { get; set; }
        public virtual ICollection<EnginesForModel> EnginesForModel { get; set; }
    }
}

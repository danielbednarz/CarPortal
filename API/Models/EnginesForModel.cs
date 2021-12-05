using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API.Entities
{
    public class EnginesForModel
    {
        public int Id { get; set; }
        public int ModelId { get; set; }
        [JsonIgnore]
        public virtual Model Model { get; set; }
        public int EngineId { get; set; }
        [JsonIgnore]
        public virtual Engine Engine { get; set; }
    }
}

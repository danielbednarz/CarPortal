using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class MemberUpdateDto
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public string EngineCapacity { get; set; }
        public int EnginePower { get; set; }
        public long Mileage { get; set; }
    }
}

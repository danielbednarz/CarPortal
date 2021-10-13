using API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class MemberUpdateDto
    {
        public int BrandId { get; set; }
        public int ModelId { get; set; }
        public string EngineCapacity { get; set; }
        public int EnginePower { get; set; }
        public long Mileage { get; set; }
    }
}

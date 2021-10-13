using API.Entities;
using System;
using System.Collections.Generic;

namespace API.DTOs
{
    public class MemberDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string PhotoUrl { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastActive { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        public int ModelId { get; set; }
        public Model Model { get; set; }
        public string EngineCapacity { get; set; }
        public int EnginePower { get; set; }
        public long Mileage { get; set; }
        public DateTime ProductionDate { get; set; }
        public ICollection<PhotoDto> Photos { get; set; }
    }
}

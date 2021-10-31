using System;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class RegisterDto
    {

        [Required]
        public string Username { get; set; }
        public int BrandId { get; set; }
        public int ModelId { get; set; }
        public int EngineId { get; set; }
        public int EnginePower { get; set; }
        public long Mileage { get; set; }
        public DateTime ProductionDate { get; set; }
        [Required]
        [StringLength(32, MinimumLength = 8)]
        public string Password { get; set; }
    }
}

using API.Entities;
using API.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API.Models
{
    public class CarInsurance
    {
        public int Id { get; set; }
        public CarInsuranceType CarInsuranceType { get; set; }
        public DateTime ExpirationDate { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Cost { get; set; }
        public string InsurerName { get; set; }
        public int UserId { get; set; }
        [JsonIgnore]
        public AppUser User { get; set; }
    }
}

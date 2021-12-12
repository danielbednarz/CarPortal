using API.Entities;
using System;
using System.Text.Json.Serialization;

namespace API.Models
{
    public class PeriodicInspection
    {
        public int Id { get; set; }
        public DateTime InspectionDate { get; set; }
        public bool isPositive { get; set; }
        public int UserId { get; set; }
        [JsonIgnore]
        public AppUser User { get; set; }
    }
}

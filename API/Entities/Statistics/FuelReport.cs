using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class FuelReport
    {
        [Key]
        public Guid Id { get; set; }
        public decimal Cost { get; set; }
        public decimal FuelAmount { get; set; }
        public decimal TraveledDistance { get; set; }
        public DateTime RefuelDate { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }

    }
}

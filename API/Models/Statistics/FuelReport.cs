using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class FuelReport
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Cost { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal FuelAmount { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal TraveledDistance { get; set; }
        public DateTime RefuelDate { get; set; }
        public int UserId { get; set; }
        public virtual AppUser User { get; set; }

    }
}

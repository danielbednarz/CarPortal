using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities.Views
{
    public class RepairReportView
    {
        public string Month { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal CostPerMonth { get; set; }
        public int UserId { get; set; }
    }
}

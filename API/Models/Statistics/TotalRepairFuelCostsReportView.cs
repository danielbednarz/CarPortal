using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities.Views
{
    public class TotalRepairFuelCostsReportView
    {
        public string Text { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Value { get; set; }
    }
}

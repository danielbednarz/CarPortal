using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities.Views
{
    public class FuelReportView
    {
        public string Month { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal AverageConsumption { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    [Keyless]
    public class CarInsuranceRemainingDays
    {
        public int RemainingDays { get; set; }
    }
}

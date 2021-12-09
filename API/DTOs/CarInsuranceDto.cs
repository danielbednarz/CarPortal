using API.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class CarInsuranceDto
    {
        public CarInsuranceType CarInsuranceType { get; set; }
        public DateTime ExpirationDate { get; set; }
        public decimal Cost { get; set; }
        public string InsurerName { get; set; }
    }
}

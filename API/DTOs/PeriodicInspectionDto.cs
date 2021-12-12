using System;

namespace API.DTOs
{
    public class PeriodicInspectionDto
    {
        public DateTime InspectionDate { get; set; }
        public bool isPositive { get; set; }
    }
}

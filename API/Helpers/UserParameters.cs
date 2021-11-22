using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Helpers
{
    public class UserParameters : PaginationParameters
    {
        public string CurrentUsername { get; set; }
        public int BrandId { get; set; }
        public int ModelId { get; set; }
        public int MinEnginePower { get; set; } = 0;
        public int MaxEnginePower { get; set; } = 1500;
        public string OrderBy { get; set; } = "CreateDateDesc";
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Helpers
{
    public class UserParameters
    {
        private const int MaxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        private int currentPageSize = 20;
        public int PageSize
        {
            get => currentPageSize;
            set => currentPageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        public string CurrentUsername { get; set; }
        public int BrandId { get; set; }
        public int ModelId { get; set; }
        public int MinEnginePower { get; set; } = 0;
        public int MaxEnginePower { get; set; } = 1500;
    }
}

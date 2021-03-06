using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Helpers
{
    public class PaginationParameters
    {
        private const int MaxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        private int currentPageSize = 20;
        public int PageSize
        {
            get => currentPageSize;
            set => currentPageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
    }
}

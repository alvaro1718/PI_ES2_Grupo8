using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PI_ES2_Grupo8.Models
{
    public class PagingViewModel
    {
        public int TotalItems { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int NumberPage => (int)Math.Ceiling((double)TotalItems / PageSize);
    }
}

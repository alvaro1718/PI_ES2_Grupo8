using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PI_ES2_Grupo8.Models
{
    public class TratamentosListViewModel
    {
        public IEnumerable<Tratamento> Tratamentos { get; set; }
        public PagingViewModel Pagination { get; set; }

        // [DisplayName("Nome")]
        public string CurrentName { get; set; }
    }
}

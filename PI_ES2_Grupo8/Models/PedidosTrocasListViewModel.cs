using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PI_ES2_Grupo8.Models
{
    public class PedidosTrocasListViewModel
    {

        public IEnumerable<Troca> PedidosTrocas { get; set; }
        public PagingViewModel Pagination { get; set; }

        //[DisplayName("")]
        public string CurrentName { get; set; }
    }
}

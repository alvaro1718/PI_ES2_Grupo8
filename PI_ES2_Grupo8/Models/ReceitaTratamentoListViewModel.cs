﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PI_ES2_Grupo8.Models
{
    public class ReceitaTratamentoListViewModel
    {
        public IEnumerable<ReceitarTratamento> ReceitaTratamento { get; set; }
        public PagingViewModel Pagination { get; set; }

       
        public string CurrentName { get; set; }
    }
}

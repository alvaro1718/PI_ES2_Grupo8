using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PI_ES2_Grupo8.Models
{
    public class ReceitarTratamento
    {
        public int ReceitarTratamentoId { get; set; }

        public int ReceitaId { get; set; }
        public Receita receita { get; set; }

        public int TratamentoId { get; set; }

        public Tratamento tratamento { get; set; }

        public DateTime DataTratamento { get; set; }
    }
}

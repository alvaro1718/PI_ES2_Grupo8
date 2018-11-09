using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PI_ES2_Grupo8.Models
{
    public class Troca
    {
        public int TrocaId { get; set; }

        public string justificacao { get; set; }

        public Enfermeiros Enfermeiros { get; set; }

        public int EnfermeirosId { get; set; }

        public HorarioServicoDomicilio HorarioServicoDomicilio { get; set; }

        public int HorarioServicoDomicilioId { get; set; }

    }
}

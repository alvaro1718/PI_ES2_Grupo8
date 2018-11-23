using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PI_ES2_Grupo8.Models
{
    public class EnfermeiroRequerente
    {
        public int EnfermeiroRequerenteId { get; set; }

        public Enfermeiros Enfermeiros { get; set; }

        public int EnfermeirosId { get; set; }

    }
}

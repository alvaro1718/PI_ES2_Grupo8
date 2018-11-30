using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PI_ES2_Grupo8.Models
{
    public class HorarioServicoDomicilio
    {
        public int HorarioServicoDomicilioId { get; set; }
        public DateTime Data { get; set; }
 
        public string HoraInicio { get; set; }

        public string HoraFim { get; set; }

        public Enfermeiros Enfermeiros { get; set; } 

        public int EnfermeirosId { get; set; }

        public ICollection <Utente> Utente { get; set; }

        public ICollection<Tratamento> Tratamentos { get; set; }

        public ICollection<Troca> Troca { get; set; } 
    }
}

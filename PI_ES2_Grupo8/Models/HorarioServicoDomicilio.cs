using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PI_ES2_Grupo8.Models
{
    public class HorarioServicoDomicilio
    {
        public int HorarioServicoDomicilioId { get; set; }
        public int Data { get; set; }

        public int Hora { get; set; }

        public Enfermeiros Enfermeiros { get; set; } 

        public ICollection <Utente> Utente { get; set; }

        public ICollection<Tratamento> Tratamentos { get; set; }
    }
}

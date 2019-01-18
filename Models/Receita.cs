using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PI_ES2_Grupo8.Models
{
    public class Receita
    {
        public int ReceitaId { get; set; }

        public int MedicoId { get; set; }
        public Medico medico { get; set; }

        public Utente utente { get; set; }

        public int UtenteId { get; set; }

        public DateTime Date { get; set; }

        public int Nreceita { get; set; }
        public ICollection<ReceitarTratamento> receitarTratamentos { get; set; }

    }
}

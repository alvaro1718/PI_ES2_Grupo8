using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PI_ES2_Grupo8.Models
{
    public class Tratamento
    {

        

        //public int ServicoId { get; set; }
        //public int MaterialId { get; set;}
        public String Discricao { get; set;}
        public int TratamentoId { get; set; }

        public Enfermeiros Enfermeiros { get; set; }
        public int EnfermeirosId { get; set; }

        public ICollection<Utente> Utentes { get; set; }
        public ICollection<Servicos> Servicos { get; set; }
        public ICollection<Material> Materials { get; set; }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PI_ES2_Grupo8.Models
{
    public class Utente
    {
        public int UtenteId { get; set; }


        public string Nome { get; set; }

        

        //public String Sexo { get; set; }

        public String Morada { get; set; }
        public String Telefone { get; set; }
        public String Email { get; set; }

        //public string TipodeTratamento { get; set; }
        public string Description { get; set; }
    }
}

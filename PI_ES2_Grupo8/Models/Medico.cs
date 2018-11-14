using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PI_ES2_Grupo8.Models
{
    public class Medico
    {
        public int MedicoId { get; set; }
        public string Nome { get; set; }
        public String Morada { get; set; }
        public String Telefone { get; set; }
        public String Email { get; set; }

        public ICollection<Receita> receitas { get; set; }

    }
}

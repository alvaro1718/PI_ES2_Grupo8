using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PI_ES2_Grupo8.Models
{
    public class Especialização
    {
        public int EspecializaçãoId { get; set; }

        [Required(ErrorMessage = "Por favor insira a Especialização")]
        public string Nome { get; set; }

    }
}

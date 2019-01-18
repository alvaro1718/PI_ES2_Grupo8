using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PI_ES2_Grupo8.Models
{
    public class Tratamento
    {

        public int TratamentoId { get; set; }
        [Required(ErrorMessage = "Por favor introduza o tratamento")]
        public String TipodeTratamento { get; set; }

        public ICollection<ReceitarTratamento> receitarTratamentos { get; set; }
    }
}

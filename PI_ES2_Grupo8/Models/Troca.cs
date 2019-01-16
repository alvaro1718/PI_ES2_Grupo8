using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PI_ES2_Grupo8.Models
{
    public class Troca
    {
        public int TrocaId { get; set; }

        [Required(ErrorMessage = "Por favor insira a justificação")]
        public string Justificação { get; set; }

        public Enfermeiros EnfermeiroRequerente { get; set; }

        public int EnfermeirosId { get; set; }

        public Enfermeiros EnfermeiroEscolhido { get; set; }
        public int EnfermeirosEId { get; set; }

        //[RegularExpression(@"((0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d{2})", ErrorMessage = "Data Invalida.")]
        public DateTime Data { get; set; }

        public HorarioTrabalho HorarioTrabalhoNovo { get; set; }

        public int HorarioTrabalhoId { get; set; }

        public HorarioTrabalho HorarioTrabalhoAntigo { get; set; }

        public int HorarioTrabalhoAntigoId { get; set; }

        public Boolean Aprovar { get; set; }


    }
}

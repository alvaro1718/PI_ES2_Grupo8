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

        public EnfermeiroRequerente EnfermeiroRequerente { get; set; }

        public int EnfermeiroRequerenteId { get; set; }

        public EnfermeiroEscolhido EnfermeiroEscolhido { get; set; }

        public int EnfermeiroEscolhidoId { get; set; }  

        //[RegularExpression(@"((0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d{2})", ErrorMessage = "Data Invalida.")]
        public DateTime Data { get; set; }

        public HorarioServicoDomicilio HorarioServicoDomicilio { get; set; }

        public int HorarioServicoDomicilioId { get; set; }



    }
}

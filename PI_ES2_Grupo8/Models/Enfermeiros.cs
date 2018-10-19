using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PI_ES2_Grupo8.Models
{
    public class Enfermeiros
    {
        private int EnfermeiroId{ get; set; }

        [Required(ErrorMessage = "Por favor insira o seu nome" )]
        private string Nome { get; set; }

        [Required(ErrorMessage = "Por favor insira o seu numero de telefone")]
        [RegularExpression(@"9\d{8}", ErrorMessage = "Numero invalido.")]
        [EmailAddress(ErrorMessage = "Numero Invalido.")]
        private string Telefone { get; set; }

        [RegularExpression(@"(\w+(\.\w+)*@[a-zA-Z_]+?\.[a-zA-Z]{2,6})",ErrorMessage = "Invalid email.")]
        [Required(ErrorMessage = "Por favor insira o seu Email")]
        private string Email { get; set; }

        [Required(ErrorMessage = "Por favor insira o sua morada")]
        private string Morada { get; set; }

        [Required(ErrorMessage = "Por favor insira a sua profissao ou especializao")]
        private string Especializacao { get; set; }
    }
}

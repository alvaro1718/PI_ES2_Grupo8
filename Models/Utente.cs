using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PI_ES2_Grupo8.Models
{
    public class Utente
    {
        public int UtenteId { get; set; }

        [Required(ErrorMessage = "Por favor introduza o seu nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Por favor introduza o seu número do utente de saúde")]
        [RegularExpression(@"[0-9]+$", ErrorMessage = "Número inválido.")]
        public string N_Utente_Saude { get; set; }
        //public String Sexo { get; set; }
        [Required(ErrorMessage = "Por favor introduza o seu endereço")]
        public String Morada { get; set; }


        [Required(ErrorMessage = "Por favor introduza o seu número de telemóvel")]
        [RegularExpression(@"9[1236]\d{7}", ErrorMessage = "Número inválido.")]
        public String Telefone { get; set; }

        [Required(ErrorMessage = "Por favor introduza o seu email")]
        //[RegularExpression(@"(\w+(\.\w+)*@[populate-zA-Z_]+?\.[populate-zA-Z]{2,6})",ErrorMessage = "email inválido.")]
        [EmailAddress(ErrorMessage = "email inválido.")]
        public String Email { get; set; }

        
        public string Problemas { get; set; }

        public ICollection<Receita> receitas {get; set;}
    }
}

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

        [Required(ErrorMessage = "Please enter your name")]
        public string Nome { get; set; }



        //public String Sexo { get; set; }
        [Required(ErrorMessage = "Please enter your address")]
        public String Morada { get; set; }


        [Required(ErrorMessage = "Please enter your phone number")]
        [RegularExpression(@"(9[1236])/d{7})", ErrorMessage = "Invalid number.")]
        public String Telefone { get; set; }

        [Required(ErrorMessage = "Please enter your email")]
        public String Email { get; set; }

        
        public string Descricao { get; set; }

        public ICollection<Receita> receitas {get; set;}
    }
}

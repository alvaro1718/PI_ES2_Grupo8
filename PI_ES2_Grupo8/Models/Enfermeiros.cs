using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PI_ES2_Grupo8.Models
{
    public class Enfermeiros
    {
        public int EnfermeirosId { get; set; }

        [Required(ErrorMessage = "Por favor insira o seu nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Por favor insira o seu numero de telefone")]
        [RegularExpression(@"(9\d{8})", ErrorMessage = "Numero invalido.")]
        public string Telefone { get; set; }

        [RegularExpression(@"(\w+(\.\w+)*@[a-zA-Z_]+?\.[a-zA-Z]{2,6})", ErrorMessage = "Invalid email.")]
        [Required(ErrorMessage = "Por favor insira o seu Email")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Por favor insira o sua morada")]
        public string Morada { get; set; }

        public Especialização Especialização { get; set; }

        public int EspecializaçãoId { get; set; }

        public ICollection<Troca> Trocas { get; set; }
        public ICollection<Troca> TrocaE { get; set; }

        public ICollection<Tratamento> Tratamentos { get; set; }


       

        

    }
}

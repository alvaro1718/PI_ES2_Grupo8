using Microsoft.Extensions.DependencyInjection;
using PI_ES2_Grupo8.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PI_ES2_Grupo8.Data
{
    public class SeedData
    {
        public static void Populate(IServiceProvider applicationServices)
        {
            using (var serviceScope = applicationServices.CreateScope())
            {
                var db = serviceScope.ServiceProvider.GetService<ServicoDomicilioDbContext>();

                if (db.Utente.Any()) return;

                db.Utente.AddRange(
                new Utente { Nome = "Pedro Martins", Morada = "Rua Rampa das Necesidades", Telefone = "921876352", Email = "pedro12@gmail.com", Description = "Difilculdades Locumoção" },
                new Utente { Nome = "João Lua", Morada = "Rua Santo Antônio", Telefone = "961234098", Email = "Lua@gmail.com", Description = "Problemas de Visão" },
                new Utente { Nome = "Rui Afonso", Morada = "Rua Tiradentes", Telefone = "911210983", Email = "r_afonso@gmail.com", Description = "Problemas de Visão" },
                new Utente { Nome = "Ana Salgado", Morada = "Rua Santa Luzia", Telefone = "933121099", Email = "ana_salg@gmail.com", Description = "Difilculdades Locumoção" },
                new Utente { Nome = "Marta Camões", Morada = "Rua Duque De Caxias", Telefone = "910993312", Email = "marta2_cam@gmail.com", Description = "Problemas de Visão" }
                );

                db.SaveChanges();

                /*if (db.Enfermeiros.Any()) return;

                db.Enfermeiros.AddRange(
                new Enfermeiros { Nome = "Paulo", Telefone = "927405851", Email = "paulo@gmail.com", Morada = "Rua Mota joao",  Especializacao = "Nenhum" },
                new Enfermeiros { Nome = "Alvaro", Telefone = "922076352", Email = "alvaro555@gmail.com", Morada = "Rua da Morte", Especializacao = "Pediatria" },
                new Enfermeiros { Nome = "João", Telefone = "921855352", Email = "joao12@gmail.com", Morada = "Rua Martinho da Rocha", Especializacao = "Enfermagem de Saúde Materna e obstetrícia" },
                new Enfermeiros { Nome = "Maria", Telefone = "921876398", Email = "maria24@gmail.com", Morada = "Rua da Boa Esperança", Especializacao = "Nenhum" },
                new Enfermeiros { Nome = "Joana", Telefone = "921876352", Email = "joana10@gmail.com", Morada = "Rua da Neves e Ceita", Especializacao = "Enfermagem de Saúde Mental e Psquiatria" }

                );


                db.SaveChanges();*/
            }
        }
    }
}

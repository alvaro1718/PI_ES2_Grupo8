using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PI_ES2_Grupo8.Models
{
    public class SeedData
    {
        public static void Populate(IServiceProvider applicationServices)
        {
            using (var serviceScope = applicationServices.CreateScope())
            {
                var db = serviceScope.ServiceProvider.GetService<UtenteDbContext>();

                if (db.Utentes.Any()) return;

                db.Utentes.AddRange(
                    new Utente { Name = "Rui", Description = "", Sexo = "Masculino", Servicos = "Medicacao" }

                );
                db.SaveChanges();
            }
        }
    }
}

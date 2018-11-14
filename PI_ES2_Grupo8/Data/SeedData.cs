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
        internal static void Populate(ServicoDomicilioDbContext db)
        {
            SeedMedico(db);
            seedUtente(db);
            seedTratamento(db);
            seedReceita(db);
            SeedReceitaTratamento(db);
        }

        private static void seedReceita(ServicoDomicilioDbContext db)
        {
            throw new NotImplementedException();
        }

        private static void SeedReceitaTratamento(ServicoDomicilioDbContext db)
        {
            throw new NotImplementedException();
        }

        private static void seedTratamento(ServicoDomicilioDbContext db)
        {
            throw new NotImplementedException();
        }

        private static void seedUtente(ServicoDomicilioDbContext db)
        {
            if (db.Utente.Any()) return;

            db.Utente.AddRange(
            new Utente { Nome = "Rui Martins", Morada = "Rua Rampa das Necesidades", Telefone = "962276352", Email = "pedro12@gmail.com", Description = "Difilculdades Locumoção" },
            new Utente { Nome = "Pedro Lua", Morada = "Rua Santo Antônio", Telefone = "923234098", Email = "Lua@gmail.com", Description = "Problemas de Visão" },
            new Utente { Nome = "Afonso Pires", Morada = "Rua Tiradentes", Telefone = "911210322", Email = "r_afonso@gmail.com", Description = "Problemas de Visão" },
            new Utente { Nome = "MafaldaCunha", Morada = "Rua Santa Luzia", Telefone = "933121099", Email = "ana_salg@gmail.com", Description = "Difilculdades Locumoção" },
            new Utente { Nome = "Marcela Bernardo", Morada = "Rua Duque De Caxias", Telefone = "910993312", Email = "marta2_cam@gmail.com", Description = "Problemas de Visão" }
            );

            db.SaveChanges();
        }

        private static void SeedMedico(ServicoDomicilioDbContext db)
        {
            if (db.Utente.Any()) return;

            db.Medico.AddRange(
            new Medico { Nome = "Pedro Martins", Morada = "Rua Rampa das Necesidades", Telefone = "921876352", Email = "pedro12@gmail.com"},
            new Medico{ Nome = "João Lua", Morada = "Rua Santo Antônio", Telefone = "961234098", Email = "Lua@gmail.com"},
            new Medico { Nome = "Rui Afonso", Morada = "Rua Tiradentes", Telefone = "911210983", Email = "r_afonso@gmail.com"},
            new Medico { Nome = "Ana Salgado", Morada = "Rua Santa Luzia", Telefone = "933121099", Email = "ana_salg@gmail.com"},
            new Medico { Nome = "Marta Camões", Morada = "Rua Duque De Caxias", Telefone = "910993312", Email = "marta2_cam@gmail.com"}
            );

            db.SaveChanges();
        }
    }
}




/* public static void Populate(IServiceProvider applicationServices)
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
            }
        }*/

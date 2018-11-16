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
        public static void Populate(ServicoDomicilioDbContext db)
        {
   
            SeedEnfermeiros(db);
            SeedTroca(db);
            SeedEspecialização(db);



        }

        

        private static Enfermeiros CreateEnfermeirosIfDoesNotExist(ServicoDomicilioDbContext db, string nome, string telefone, string email, string morada, Especialização especialização)
        {
            Enfermeiros enfermeiros  = db.Enfermeiros.SingleOrDefault(b => b.Nome == nome);

            if (enfermeiros == null)
            {
                db.Enfermeiros.Add(new Enfermeiros { Nome = nome, Telefone = telefone, Email = email, Morada = morada,
                    EspecializaçãoId = especialização.EspecializaçãoId });
            }

            return enfermeiros;

        }

        private static void SeedEnfermeiros(ServicoDomicilioDbContext db)
        {
            Especialização especialização = GetEspecializaçãoCreatingIfNeed(db, "Pediatria");
            CreateEnfermeirosIfDoesNotExist(db, "Paulo", "927405851", "paulo@gmail.com", "Rua Mota joao", especialização);
            CreateEnfermeirosIfDoesNotExist(db, "Alvaro", "922076352", "alvaro555@gmail.com", "Rua da Liberdade", especialização);

            especialização = GetEspecializaçãoCreatingIfNeed(db, "Enfermagem de Saúde Mental e Psquiatria");
            CreateEnfermeirosIfDoesNotExist(db, "Paulo", "927405851", "paulo@gmail.com", "Rua Mota joao", especialização);
            CreateEnfermeirosIfDoesNotExist(db, "Maria", "921876398", "maria24@gmail.com", "Rua da Boa Esperança", especialização);

            db.SaveChanges();


        }

        private static Especialização GetEspecializaçãoCreatingIfNeed(ServicoDomicilioDbContext db, string nome)
        {
            Especialização especialização = db.Especialização.SingleOrDefault(a => a.Nome == nome);

            if (especialização == null)
            {
                especialização = new Especialização { Nome = nome };
                db.Add(especialização);
                db.SaveChanges();
            }

            return especialização;
        }


        private static void SeedEspecialização(ServicoDomicilioDbContext db)
        {
            if (db.Especialização.Any()) return;

            db.Especialização.AddRange(
                new Especialização { Nome = "Pediatria" },
                new Especialização { Nome = "Enfermagem de Saúde Materna e obstetrícia" },
                new Especialização { Nome = "Enfermagem de Saúde Mental e Psquiatria" }   
            );

            db.SaveChanges();
        }

        private static void SeedTroca(ServicoDomicilioDbContext db)
        {
            throw new NotImplementedException();
        }

       

    }
}

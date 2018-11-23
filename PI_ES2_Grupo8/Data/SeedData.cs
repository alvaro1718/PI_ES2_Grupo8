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
            SeedEnfermeiroRequerente(db);
            SeedEnfermeiroEscolhido(db);


        }

        private static void SeedEnfermeiroRequerente(ServicoDomicilioDbContext db)
        {
            if (db.EnfermeiroRequerente.Any()) return;

            Enfermeiros enfermeiros = db.Enfermeiros.SingleOrDefault(b => b.Nome == "Paulo");
            db.EnfermeiroRequerente.Add(new EnfermeiroRequerente { EnfermeirosId = enfermeiros.EnfermeirosId });

            db.Enfermeiros.SingleOrDefault(b => b.Nome == "Alvaro");
            db.EnfermeiroRequerente.Add(new EnfermeiroRequerente { EnfermeirosId = enfermeiros.EnfermeirosId });

        }

        private static void SeedEnfermeiroEscolhido(ServicoDomicilioDbContext db)
        {
            if (db.EnfermeiroEscolhido.Any()) return;

            Enfermeiros enfermeiros = db.Enfermeiros.SingleOrDefault(b => b.Nome == "João");
            db.EnfermeiroEscolhido.Add(new EnfermeiroEscolhido { EnfermeirosId = enfermeiros.EnfermeirosId });

            db.Enfermeiros.SingleOrDefault(b => b.Nome == "Maria");
            db.EnfermeiroEscolhido.Add(new EnfermeiroEscolhido { EnfermeirosId = enfermeiros.EnfermeirosId });
        }

        private static EnfermeiroRequerente CreateEnfermeiroRequerenteIfDoesNotExist(ServicoDomicilioDbContext db, Enfermeiros enfermeiros)
        {
            EnfermeiroRequerente enfermeiroRequerente = db.EnfermeiroRequerente.SingleOrDefault(b => b.EnfermeirosId == enfermeiros.EnfermeirosId);

            if (enfermeiroRequerente == null)
            {

                db.EnfermeiroRequerente.Add(new EnfermeiroRequerente { EnfermeirosId = enfermeiros.EnfermeirosId });

            }
            return enfermeiroRequerente;
        }
        private static EnfermeiroEscolhido CreateEnfermeiroEscolhidoIfDoesNotExist(ServicoDomicilioDbContext db, Enfermeiros enfermeiros)
        {
            EnfermeiroEscolhido enfermeiroEscolhido  = db.EnfermeiroEscolhido.SingleOrDefault(b => b.EnfermeirosId == enfermeiros.EnfermeirosId);

            if (enfermeiroEscolhido == null)
            {

                db.EnfermeiroRequerente.Add(new EnfermeiroRequerente { EnfermeirosId = enfermeiros.EnfermeirosId });

            }
            return enfermeiroEscolhido;
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
            CreateEnfermeirosIfDoesNotExist(db, "João", "921402734", "joao@gmail.com", "Rua Madre de Deus", especialização);
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
                new Especialização { Nome = "Psquiatria" }   
            );

            db.SaveChanges();
        }

        private static Troca GetTrocaCreatingIfNeed(ServicoDomicilioDbContext db, string justificação, EnfermeiroRequerente enfermeiroRequerente,
            EnfermeiroEscolhido enfermeiroEscolhido, DateTime data, HorarioServicoDomicilio horarioServicoDomicilio)
        {
            Troca troca = db.Troca.SingleOrDefault(b => b.Justificação == justificação);

            if (troca == null)
            {
                db.Troca.Add(new Troca
                {
                    Justificação = justificação,
                    EnfermeiroRequerenteId = enfermeiroRequerente.EnfermeiroRequerenteId,
                    EnfermeiroEscolhidoId = enfermeiroEscolhido.EnfermeiroEscolhidoId,
                    Data = data,
                    HorarioServicoDomicilioId = horarioServicoDomicilio.HorarioServicoDomicilioId
                });
            }
            return troca;
        }

        private static void SeedTroca(ServicoDomicilioDbContext db)
        {
            DateTime dataTroca;
            HorarioServicoDomicilio horarioServicoDomicilio = new HorarioServicoDomicilio();
            DateTime data = DateTime.Today;
            dataTroca = data;

                //data.ToString("dd MM YYYY");

        
            Enfermeiros enfermeiro1 = db.Enfermeiros.SingleOrDefault(b => b.Nome == "João");
            Enfermeiros enfermeiro2 = db.Enfermeiros.SingleOrDefault(b => b.Nome == "Paulo");
            EnfermeiroRequerente enfermeiroRequerente = CreateEnfermeiroRequerenteIfDoesNotExist(db,enfermeiro1);
            EnfermeiroEscolhido enfermeiroEscolhido = CreateEnfermeiroEscolhidoIfDoesNotExist(db,enfermeiro2);
            GetTrocaCreatingIfNeed(db, "Doente", enfermeiroRequerente, enfermeiroEscolhido, dataTroca, horarioServicoDomicilio);
           


        }

       

    }
}

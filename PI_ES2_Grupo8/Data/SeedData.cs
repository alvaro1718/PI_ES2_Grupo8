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
            SeedEspecialização(db);
            SeedEnfermeiros(db);
            //SeedEnfermeiroRequerente(db);
            //SeedEnfermeiroEscolhido(db);
            //SeedTroca(db);
            SeedHorarioTrabalho(db);
          
           


        }

        

        /*private static void SeedEnfermeiroRequerente(ServicoDomicilioDbContext db)
        {
            if (db.EnfermeiroRequerente.Any()) return;

            Enfermeiros enfermeiros = db.Enfermeiros.SingleOrDefault(b => b.Nome == "Paulo");
            db.EnfermeiroRequerente.Add(new EnfermeiroRequerente { EnfermeirosId = enfermeiros.EnfermeirosId});

            db.SaveChanges();
            //   db.Enfermeiros.SingleOrDefault(b => b.Nome == "Alvaro");
            //   db.EnfermeiroRequerente.Add(new EnfermeiroRequerente { EnfermeirosId = enfermeiros.EnfermeirosId });

        }

        private static void SeedEnfermeiroEscolhido(ServicoDomicilioDbContext db)
        {
            if (db.EnfermeiroEscolhido.Any()) return;

            Enfermeiros enfermeiros = db.Enfermeiros.SingleOrDefault(b => b.Nome == "João");
            db.EnfermeiroEscolhido.Add(new EnfermeiroEscolhido { EnfermeirosId = enfermeiros.EnfermeirosId });
            db.SaveChanges();
            //db.Enfermeiros.SingleOrDefault(b => b.Nome == "Maria");
            // db.EnfermeiroEscolhido.Add(new EnfermeiroEscolhido { EnfermeirosId = enfermeiros.EnfermeirosId });
        }

        private static EnfermeiroRequerente CreateEnfermeiroRequerenteIfDoesNotExist(ServicoDomicilioDbContext db, Enfermeiros enfermeiros)
        {
            EnfermeiroRequerente enfermeiroRequerente = db.EnfermeiroRequerente.SingleOrDefault(b => b.EnfermeirosId == enfermeiros.EnfermeirosId);

            if (enfermeiroRequerente == null)
            {

                db.EnfermeiroRequerente.Add(new EnfermeiroRequerente { EnfermeirosId = enfermeiros.EnfermeirosId });
                db.SaveChanges();
            }
            return enfermeiroRequerente;
        }
        private static EnfermeiroEscolhido CreateEnfermeiroEscolhidoIfDoesNotExist(ServicoDomicilioDbContext db, Enfermeiros enfermeiros)
        {
            EnfermeiroEscolhido enfermeiroEscolhido  = db.EnfermeiroEscolhido.SingleOrDefault(b => b.EnfermeirosId == enfermeiros.EnfermeirosId);

            if (enfermeiroEscolhido == null)
            {

                db.EnfermeiroRequerente.Add(new EnfermeiroRequerente { EnfermeirosId = enfermeiros.EnfermeirosId });
                db.SaveChanges();
            }
            return enfermeiroEscolhido;
        }*/

        private static HorarioTrabalho HorarioTrabalhoIfDoesNotExist(ServicoDomicilioDbContext db, DateTime data, string horaInicio, 
            string horaFim, Enfermeiros enfermeiro)
        {
            HorarioTrabalho horarioTrabalho = new HorarioTrabalho();
            if(horarioTrabalho == null)
            {
                db.HorarioTrabalho.Add(new HorarioTrabalho
                {
                    Data = data,
                    HoraInicio = horaInicio,
                    HoraFim = horaFim,
                    EnfermeirosId = enfermeiro.EnfermeirosId
                });
                db.SaveChanges();
            }
            return horarioTrabalho;

        }

        private static void SeedHorarioTrabalho(ServicoDomicilioDbContext db)
        {
            DateTime data = DateTime.Today;
            Enfermeiros enfermeiro1 = db.Enfermeiros.SingleOrDefault(b => b.Nome == "Paulo");
            HorarioTrabalhoIfDoesNotExist(db, data, "8h:00", "18h:00", enfermeiro1);



        }


        private static Enfermeiros CreateEnfermeirosIfDoesNotExist(ServicoDomicilioDbContext db, string nome, string telefone, string email, string morada, Especialização especialização)
        {
            Enfermeiros enfermeiros  = db.Enfermeiros.SingleOrDefault(b => b.Nome == nome);

            if (enfermeiros == null)
            {
                    db.Enfermeiros.Add(new Enfermeiros { Nome = nome, Telefone = telefone, Email = email, Morada = morada,
                    EspecializaçãoId = especialização.EspecializaçãoId });
                db.SaveChanges();
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
            EnfermeiroEscolhido enfermeiroEscolhido, DateTime data, HorarioTrabalho horarioTrabalho)
        {
            Troca troca = db.Troca.SingleOrDefault(b => b.Justificação == justificação);

            if (troca == null)
            {
                db.Troca.Add(new Troca
                {
                    Justificação = justificação,
                    //EnfermeiroRequerenteId = enfermeiroRequerente.EnfermeiroRequerenteId,
                    //EnfermeiroEscolhidoId = enfermeiroEscolhido.EnfermeiroEscolhidoId,
                    Data = data,
                    //HorarioServicoDomicilioId = horarioTrabalho.HorarioTrabalhoId
                });
                //db.SaveChanges();
            }
            return troca;
        }

        /*private static void SeedTroca(ServicoDomicilioDbContext db)
        {
            DateTime dataTroca;
            HorarioTrabalho horarioTrabalho = new HorarioTrabalho();
            DateTime data = DateTime.Now;
            dataTroca = data;

                //data.ToString("dd MM YYYY");

        
            Enfermeiros enfermeiro1 = db.Enfermeiros.SingleOrDefault(b => b.Nome == "Paulo");
            Enfermeiros enfermeiro2 = db.Enfermeiros.SingleOrDefault(b => b.Nome == "João");
            EnfermeiroRequerente enfermeiroRequerente = CreateEnfermeiroRequerenteIfDoesNotExist(db,enfermeiro1);
            EnfermeiroEscolhido enfermeiroEscolhido = CreateEnfermeiroEscolhidoIfDoesNotExist(db,enfermeiro2);
            GetTrocaCreatingIfNeed(db, "Doente", enfermeiroRequerente, enfermeiroEscolhido, dataTroca, horarioTrabalho);
           


        }*/
        



    }
}

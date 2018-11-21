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
        public static int nreceita = 1;
        private static void SeedReceitaTratamento(ServicoDomicilioDbContext db)
        {
         
            if (db.ReceitarTratamento.Any()) return;

            Tratamento tratamento = GetTratamentoCreatingIfNeed(db, "Ajudar na Locomoção");

           // Medico medico = GetMedicoCreatingIfNeed(db, "Pedro Martins", "Rua dos Silva", "921876352", "Martins122@gmail.com");

            //Utente utente=GetUtenteCreatingIfNeed(db, "Rui Martins", "Rua Rampa das Necesidades", "962276352", "pedro12@gmail.com", "Difilculdades Locumoção");
            Receita receita =db.Receita.SingleOrDefault(b => b.Nreceita == 0);//CreateReceitaIfDoesNotExist(db, medico, utente,nreceia);
            db.ReceitarTratamento.Add(new ReceitarTratamento { ReceitaId = receita.ReceitaId,TratamentoId = tratamento.TratamentoId});

            tratamento = GetTratamentoCreatingIfNeed(db, "Vacinar");
            db.ReceitarTratamento.Add(new ReceitarTratamento { ReceitaId = receita.ReceitaId, TratamentoId = tratamento.TratamentoId });
            db.SaveChanges();
        }
   
        private static void seedReceita(ServicoDomicilioDbContext db)
        {
            Medico medico = GetMedicoCreatingIfNeed(db,"Pedro Martins", "Rua dos Silva", "921876352", "Martins122@gmail.com");
            Utente utente= GetUtenteCreatingIfNeed(db, "Rui Martins",  "Rua Rampa das Necesidades",  "962276352", "pedro12@gmail.com","Difilculdades Locumoção");

            // DateTime date =DateTime.Now.Date;
           // nreceia++;
            CreateReceitaIfDoesNotExist(db,medico,utente,nreceita);
            
        }

        private static Receita CreateReceitaIfDoesNotExist(ServicoDomicilioDbContext db,  Medico medico,Utente utente,int Nreceita)
        {
            DateTime Date = DateTime.Today;
            Receita receita = db.Receita.SingleOrDefault(b => b.Nreceita ==Nreceita);
            if (receita == null)
            {
                Nreceita++;
                db.Receita.Add(new Receita { MedicoId = medico.MedicoId, UtenteId=utente.UtenteId,Date=Date,Nreceita=Nreceita});
                db.SaveChanges();
            }

            return receita;
        }

        private static Medico GetMedicoCreatingIfNeed(ServicoDomicilioDbContext db, string nome, string morada,string telefone,string email)
        {
            Medico medico = db.Medico.SingleOrDefault(a => a.Nome == nome);

            if (medico == null)
            {
                medico = new Medico { Nome = nome, Morada = morada, Telefone = telefone, Email =email };
                db.Add(medico);
                db.SaveChanges();
            }

            return medico;
        }

        private static Utente GetUtenteCreatingIfNeed(ServicoDomicilioDbContext db, string nome, string morada, string telefone, string email,string discricao)
        {
            Utente utente = db.Utente.SingleOrDefault(a => a.Nome == nome);

            if (utente == null)
            {
               utente = new Utente { Nome = nome, Morada = morada, Telefone = telefone, Email = email, Descricao=discricao };
                db.Add(utente);
                db.SaveChanges();
            }

            return utente;
        }

    

        private static void seedTratamento(ServicoDomicilioDbContext db)
        {
            if (db.Tratamento.Any()) return;

            db.Tratamento.AddRange(
                new Tratamento { TipodeTratamento = "Vacinar" },
                new Tratamento { TipodeTratamento = "Medicar" },
                new Tratamento { TipodeTratamento = "Alimentar" },
                new Tratamento { TipodeTratamento = "Higienizar" },
                new Tratamento { TipodeTratamento = "Ajudar na Locomoção" }


            );

            db.SaveChanges();
        }
        private static Tratamento GetTratamentoCreatingIfNeed(ServicoDomicilioDbContext db, string tipotratamento)
        {
            Tratamento tratamento = db.Tratamento.SingleOrDefault(t => t.TipodeTratamento == tipotratamento);

            if (tratamento == null)
            {
                tratamento = new Tratamento { TipodeTratamento = tipotratamento };
                db.Add(tratamento);
                db.SaveChanges();
            }

            return tratamento;
        }
        private static void seedUtente(ServicoDomicilioDbContext db)
        {
            if (db.Utente.Any()) return;

            db.Utente.AddRange(
            new Utente { Nome = "Rui Martins", Morada = "Rua Rampa das Necesidades", Telefone = "962276352", Email = "pedro12@gmail.com", Descricao = "Difilculdades Locomoção" },
            new Utente { Nome = "Pedro Lua", Morada = "Rua Santo Antônio", Telefone = "923234098", Email = "LuaP@gmail.com", Descricao = "Problemas de Visão" },
            new Utente { Nome = "Afonso Pires", Morada = "Rua Tiradentes", Telefone = "911210322", Email = "p_afonso@gmail.com", Descricao = "Problemas de Visão" },
            new Utente { Nome = "Mafalda Cunha", Morada = "Rua Santa Luzia", Telefone = "933121099", Email = "maf_cunha@gmail.com", Descricao = "Difilculdades Locomoção" },
            new Utente { Nome = "Marcela Bernardo", Morada = "Rua Duque De Caxias", Telefone = "910993312", Email = "marta2_cam@gmail.com", Descricao = "Problemas de Visão" }
            );

            db.SaveChanges();
        }

        private static void SeedMedico(ServicoDomicilioDbContext db)
        {
            if (db.Utente.Any()) return;

            db.Medico.AddRange(
            new Medico { Nome = "Pedro Martins", Morada = "Rua dos Silva", Telefone = "921876352", Email = "Martins122@gmail.com" },
            new Medico{ Nome = "João Lua", Morada = "Rua Nova da Estação", Telefone = "961234098", Email = "Lua@gmail.com"},
            new Medico { Nome = "Rui Afonso", Morada = "Rua São Vicente", Telefone = "916710983", Email = "r_afonso@gmail.com"},
            new Medico { Nome = "Ana Salgado", Morada = "Rua Santa Madalena", Telefone = "933124559", Email = "ana_salg@gmail.com"},
            new Medico { Nome = "Marta Camões", Morada = "Rua Pires de Lima", Telefone = "916653312", Email = "marta2_cam@gmail.com"}
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

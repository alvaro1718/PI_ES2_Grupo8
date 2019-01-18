using Microsoft.AspNetCore.Identity;
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
        public static Boolean populate = true;
        
        private const string ROLE_ADMINISTRATOR = "Administrator";
        private const string ROLE_MEDICO = "Medico";
        private const string ROLE_UTENTE = "Utente";
private const string ROLE_ENFERMEIRO = "Enfermeiro";
        internal static void Populate(ServicoDomicilioDbContext db)
        {
            if (populate ==true)
            {
                SeedMedico(db);
                seedUtente(db);
                seedTratamento(db);
                seedReceita(db);
                SeedReceitaTratamento(db);

		SeedEspecialização(db);
            SeedEnfermeiros(db);
            //SeedEnfermeiroRequerente(db);
            //SeedEnfermeiroEscolhido(db);
            //SeedTroca(db);
            SeedHorarioTrabalho(db);
            }
        }

        private static async void MakeSureRoleExistsAsync(RoleManager<IdentityRole> roleManager, string role)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        public static async Task CreateRolesAndUsersAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            const string ADMIN_USER = "admin@noemail.com"; 
            const string ADMIN_PASSWORD = "sECRET$123";

            MakeSureRoleExistsAsync(roleManager, ROLE_ADMINISTRATOR);
            MakeSureRoleExistsAsync(roleManager, ROLE_MEDICO);
            MakeSureRoleExistsAsync(roleManager, ROLE_UTENTE);
MakeSureRoleExistsAsync(roleManager, ROLE_ENFERMEIRO);
            IdentityUser admin = await userManager.FindByNameAsync(ADMIN_USER);
            if (admin == null)
            {
                admin = new IdentityUser { UserName = ADMIN_USER };
                await userManager.CreateAsync(admin, ADMIN_PASSWORD);
            }

            if (!await userManager.IsInRoleAsync(admin, ROLE_ADMINISTRATOR))
            {
                await userManager.AddToRoleAsync(admin, ROLE_ADMINISTRATOR);
            }
        }

        public static async Task CreateTestUsersAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            const string MEDICO_USER = "Lua@gmail.com";
            const string MEDICO_PASSWORD = "sECREDO$123";
            const string UTENTE_USER = "pedro12@gmail.com";
            const string UTENTE_PASSWORD = "sECREDO$123";
const string ENFERMEIRO_USER = "paulo@gmail.com";
            const string ENFERMEIRO_PASSWORD = "sECREDO$123";

            const string ENFERMEIRO_USER1 = "alvaro555@gmail.com";
            const string ENFERMEIRO_PASSWORD1 = "aLVARO$124";

            const string ENFERMEIRO_USER2 = "maria24@gmail.com";
            const string ENFERMEIRO_PASSWORD2 = "mARIA$125";
            IdentityUser medico = await userManager.FindByNameAsync(MEDICO_USER);
            IdentityUser utente = await userManager.FindByNameAsync(UTENTE_USER);
              IdentityUser enfermeiro = await userManager.FindByNameAsync(ENFERMEIRO_USER);
if (medico == null)
            {
                medico = new IdentityUser { UserName = MEDICO_USER };
                await userManager.CreateAsync(medico, MEDICO_PASSWORD);
            }

            if (!await userManager.IsInRoleAsync(medico, ROLE_MEDICO))
            {
                await userManager.AddToRoleAsync(medico, ROLE_MEDICO);
            }

            if (utente == null)
            {
                utente = new IdentityUser { UserName = UTENTE_USER };
                await userManager.CreateAsync(utente, UTENTE_PASSWORD);
            }

            if (!await userManager.IsInRoleAsync(utente, ROLE_UTENTE))
            {
                await userManager.AddToRoleAsync(utente, ROLE_UTENTE);
            }

if (enfermeiro == null)
            {
                enfermeiro = new IdentityUser { UserName = ENFERMEIRO_USER };
                await userManager.CreateAsync(enfermeiro, ENFERMEIRO_PASSWORD);

                enfermeiro = new IdentityUser { UserName = ENFERMEIRO_USER1 };
                await userManager.CreateAsync(enfermeiro, ENFERMEIRO_PASSWORD1);

                enfermeiro = new IdentityUser { UserName = ENFERMEIRO_USER2 };
                await userManager.CreateAsync(enfermeiro, ENFERMEIRO_PASSWORD2);
            }

            if (!await userManager.IsInRoleAsync(enfermeiro, ROLE_ENFERMEIRO))
            {
                await userManager.AddToRoleAsync(enfermeiro, ROLE_ENFERMEIRO);
            }
        }
        public static int nreceita =0;
        private static void SeedReceitaTratamento(ServicoDomicilioDbContext db)
        {
         
            if (db.ReceitarTratamento.Any()) return;

            Tratamento tratamento = GetTratamentoCreatingIfNeed(db, "Ajudar na Locomoção");

           // Medico medico = GetMedicoCreatingIfNeed(db, "Pedro Martins", "Rua dos Silva", "921876352", "Martins122@gmail.com");

            //Utente utente=GetUtenteCreatingIfNeed(db, "Rui Martins", "Rua Rampa das Necesidades", "962276352", "pedro12@gmail.com", "Difilculdades Locumoção");
            Receita receita =db.Receita.SingleOrDefault(b => b.Nreceita == nreceita);//CreateReceitaIfDoesNotExist(db, medico, utente,nreceia);
            db.ReceitarTratamento.Add(new ReceitarTratamento { ReceitaId = receita.ReceitaId,TratamentoId = tratamento.TratamentoId});

            tratamento = GetTratamentoCreatingIfNeed(db, "Vacinar");
            db.ReceitarTratamento.Add(new ReceitarTratamento { ReceitaId = receita.ReceitaId, TratamentoId = tratamento.TratamentoId });
            db.SaveChanges();
        }



        private static void seedReceita(ServicoDomicilioDbContext db)
        {
            Medico medico = GetMedicoCreatingIfNeed(db,"Pedro Martins", "Rua dos Silva", "921876352", "Martins122@gmail.com");
            Utente utente= GetUtenteCreatingIfNeed(db, "Rui Martins", "53423234", "Rua Rampa das Necesidades",  "962276352", "pedro12@gmail.com","Difilculdades Locumoção");

            // DateTime date =DateTime.Now.Date;
           // nreceia++;
            CreateReceitaIfDoesNotExist(db,medico,utente);
            
        }

        private static Receita CreateReceitaIfDoesNotExist(ServicoDomicilioDbContext db,  Medico medico,Utente utente)
        {
            DateTime Date = DateTime.Now;
            Receita receita = db.Receita.SingleOrDefault(b => b.Nreceita ==nreceita);
            if (receita == null)
            {
                
                nreceita++;
                db.Receita.Add(new Receita { MedicoId = medico.MedicoId, UtenteId=utente.UtenteId,Date=Date,Nreceita=nreceita});
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

        private static Utente GetUtenteCreatingIfNeed(ServicoDomicilioDbContext db, string nome,string NutenteSaude, string morada, string telefone, string email,string discricao)
        {
            Utente utente = db.Utente.SingleOrDefault(a => a.Nome == nome);

            if (utente == null)
            {
               utente = new Utente { Nome = nome,N_Utente_Saude=NutenteSaude, Morada = morada, Telefone = telefone, Email = email, Problemas=discricao };
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
            new Utente { Nome = "Rui Martins",N_Utente_Saude="53423234", Morada = "Rua Rampa das Necesidades", Telefone = "962276352", Email = "pedro12@gmail.com", Problemas = "Difilculdades Locomoção" },
            new Utente { Nome = "Pedro Lua", N_Utente_Saude = "234342323", Morada = "Rua Santo Antônio", Telefone = "923234098", Email = "LuaP@gmail.com", Problemas = "Problemas de Visão" },
            new Utente { Nome = "Afonso Pires", N_Utente_Saude = "76289634", Morada = "Rua Tiradentes", Telefone = "911210322", Email = "p_afonso@gmail.com", Problemas = "Problemas de Visão" },
            new Utente { Nome = "Mafalda Cunha", N_Utente_Saude = "53090034", Morada = "Rua Santa Luzia", Telefone = "933121099", Email = "maf_cunha@gmail.com", Problemas = "Difilculdades Locomoção" },
            new Utente { Nome = "Marcela Bernardo", N_Utente_Saude = "534354545", Morada = "Rua Duque De Caxias", Telefone = "910993312", Email = "marta2_cam@gmail.com", Problemas = "Problemas de Visão" }
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
            CreateEnfermeirosIfDoesNotExist(db, "Paulo Gomes De Almeida", "927405851", "paulo@gmail.com", "Rua Mota joao", especialização);
            CreateEnfermeirosIfDoesNotExist(db, "Alvaro Silva Dos Santos", "922076352", "alvaro555@gmail.com", "Rua da Liberdade", especialização);

            especialização = GetEspecializaçãoCreatingIfNeed(db, "Enfermagem de Saúde Mental e Psquiatria");
            CreateEnfermeirosIfDoesNotExist(db, "João Paulo Marques", "921402734", "joao@gmail.com", "Rua Madre de Deus", especialização);
            CreateEnfermeirosIfDoesNotExist(db, "Maria Dos Anjos Pereira", "921876398", "maria24@gmail.com", "Rua da Boa Esperança", especialização);

            especialização = GetEspecializaçãoCreatingIfNeed(db, "Pediatria");
            CreateEnfermeirosIfDoesNotExist(db, "Manuel Monte Negro De Melo", "934570452", "melo@gmail.com", "Rua Evaristo Da Silva", especialização);
            CreateEnfermeirosIfDoesNotExist(db, "Joana Barreto Rita", "921876352", "joana10@gmail.com", "Rua da Neves e Ceita", especialização);
            
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
       

        /*private static Troca GetTrocaCreatingIfNeed(ServicoDomicilioDbContext db, string justificação, EnfermeiroRequerente,
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




/*public static void Populate(IServiceProvider applicationServices)
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
        }
        */

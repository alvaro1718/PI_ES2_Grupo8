using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PI_ES2_Grupo8.Models;

namespace PI_ES2_Grupo8.Models
{
    public class ServicoDomicilioDbContext : DbContext
    {
        public ServicoDomicilioDbContext (DbContextOptions<ServicoDomicilioDbContext> options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Troca>().HasKey(o => new { o.TrocaId});


            modelBuilder.Entity<Troca>()
                .HasOne(b => b.HorarioTrabalhoNovo)
                .WithMany(a => a.Trocas)
                .HasForeignKey(b => b.HorarioTrabalhoId)
                .OnDelete(DeleteBehavior.ClientSetNull); // prevent cascade delete


            modelBuilder.Entity<Troca>()
               .HasOne(tr => tr.HorarioTrabalhoAntigo)
               .WithMany(r=>r.Trocass)
               .HasForeignKey(tr => tr.HorarioTrabalhoAntigoId)
                 .OnDelete(DeleteBehavior.ClientSetNull);


            modelBuilder.Entity<Troca>()
                 .HasOne(bc => bc.EnfermeiroRequerente)
                 .WithMany(c => c.Trocas)
                 .HasForeignKey(bc => bc.EnfermeirosId)
                 .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Troca>()
                 .HasOne(cb => cb.EnfermeiroEscolhido)
                 .WithMany(b => b.TrocaE)
                 .HasForeignKey(cb => cb.EnfermeirosEId)
                 .OnDelete(DeleteBehavior.ClientSetNull);


            modelBuilder.Entity<Enfermeiros>()
               .HasOne(b => b.Especialização)
               .WithMany(a => a.Enfermeiros)
               .HasForeignKey(b => b.EspecializaçãoId)
               .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<ReceitarTratamento>().HasKey(o => new { o.ReceitarTratamentoId});
            //.HasKey(o => new { o.ReceitaId, o.TratamentoId });


            modelBuilder.Entity<ReceitarTratamento>()
                .HasOne(bc => bc.receita)
                .WithMany(b => b.receitarTratamentos)
                .HasForeignKey(bc => bc.ReceitaId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ReceitarTratamento>()
                .HasOne(bc => bc.tratamento)
                .WithMany(c => c.receitarTratamentos)
                .HasForeignKey(bc => bc.TratamentoId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Receita>()
                .HasOne(b => b.medico)
                .WithMany(a => a.receitas)
                .HasForeignKey(b => b.MedicoId)
                .OnDelete(DeleteBehavior.ClientSetNull); 

            modelBuilder.Entity<Receita>()
               .HasOne(b => b.utente)
               .WithMany(a => a.receitas)
               .HasForeignKey(b => b.UtenteId)
               .OnDelete(DeleteBehavior.ClientSetNull);

           //modelBuilder.Entity<Receita>().HasMany<DateTime>(DateTime da);
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<PI_ES2_Grupo8.Models.Utente> Utente { get; set; }

        public DbSet<PI_ES2_Grupo8.Models.Enfermeiros> Enfermeiros { get; set; }
        public DbSet<PI_ES2_Grupo8.Models.Tratamento> Tratamento { get; set; }

        public DbSet<PI_ES2_Grupo8.Models.HorarioServicoDomicilio> HorarioServicoDomicilio { get; set; }

        public DbSet<PI_ES2_Grupo8.Models.Medico> Medico { get; set; }

        public DbSet<PI_ES2_Grupo8.Models.Receita> Receita { get; set; }

        public DbSet<PI_ES2_Grupo8.Models.ReceitarTratamento> ReceitarTratamento { get; set; }

       
    
	public DbSet<PI_ES2_Grupo8.Models.HorarioTrabalho> HorarioTrabalho { get; set; }

        public DbSet<PI_ES2_Grupo8.Models.Troca> Troca { get; set; }

        public DbSet<PI_ES2_Grupo8.Models.Especialização> Especialização { get; set; }

      //  public DbSet<PI_ES2_Grupo8.Models.EnfermeiroEscolhido> EnfermeiroEscolhido { get; set; }

       // public DbSet<PI_ES2_Grupo8.Models.EnfermeiroRequerente> EnfermeiroRequerente { get; set; }
}
}

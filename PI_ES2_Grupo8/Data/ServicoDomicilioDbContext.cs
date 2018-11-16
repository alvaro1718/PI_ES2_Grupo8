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
            
            modelBuilder.Entity<ReceitarTratamento>()
                .HasKey(o => new { o.ReceitaId, o.TratamentoId });

          
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

        public DbSet<PI_ES2_Grupo8.Models.Material> Material { get; set; }
    }
}

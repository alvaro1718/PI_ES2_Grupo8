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


           /* modelBuilder.Entity<Troca>()
               .HasOne(tr => tr.EnfermeiroRequerente)
               .WithMany(r=>r.Trocas)
               .HasForeignKey(tr => tr.EnfermeiroRequerenteId)
                 .OnDelete(DeleteBehavior.ClientSetNull);*/


           /* modelBuilder.Entity<Troca>()
                 .HasOne(bc => bc.EnfermeiroEscolhido)
                 .WithMany(c => c.Trocas)
                 .HasForeignKey(bc => bc.EnfermeiroEscolhidoId)
                 .OnDelete(DeleteBehavior.ClientSetNull); */

         

            modelBuilder.Entity<Enfermeiros>()
               .HasOne(b => b.Especialização)
               .WithMany(a => a.Enfermeiros)
               .HasForeignKey(b => b.EspecializaçãoId)
               .OnDelete(DeleteBehavior.ClientSetNull);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<PI_ES2_Grupo8.Models.Utente> Utente { get; set; }

        public DbSet<PI_ES2_Grupo8.Models.Enfermeiros> Enfermeiros { get; set; }
        public DbSet<PI_ES2_Grupo8.Models.Tratamento> Tratamento { get; set; }

        public DbSet<PI_ES2_Grupo8.Models.HorarioTrabalho> HorarioTrabalho { get; set; }

        public DbSet<PI_ES2_Grupo8.Models.Troca> Troca { get; set; }

        public DbSet<PI_ES2_Grupo8.Models.Especialização> Especialização { get; set; }

        public DbSet<PI_ES2_Grupo8.Models.EnfermeiroEscolhido> EnfermeiroEscolhido { get; set; }

        public DbSet<PI_ES2_Grupo8.Models.EnfermeiroRequerente> EnfermeiroRequerente { get; set; }
    }
}

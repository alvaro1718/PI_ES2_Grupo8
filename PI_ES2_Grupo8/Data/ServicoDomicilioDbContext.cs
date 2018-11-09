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

        public DbSet<PI_ES2_Grupo8.Models.Utente> Utente { get; set; }

        
        public DbSet<PI_ES2_Grupo8.Models.Tratamento> Tratamento { get; set; }

        public DbSet<PI_ES2_Grupo8.Models.HorarioServicoDomicilio> HorarioServicoDomicilio { get; set; }

        public DbSet<PI_ES2_Grupo8.Models.Troca> Troca { get; set; }

        public DbSet<PI_ES2_Grupo8.Models.Enfermeiros> Enfermeiros { get; set; }
    }
}

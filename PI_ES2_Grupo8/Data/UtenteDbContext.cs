using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PI_ES2_Grupo8.Models
{
    public class UtenteDbContext : DbContext
    {
        public UtenteDbContext (DbContextOptions<UtenteDbContext> options)
            : base(options)
        {
        }

        public DbSet<PI_ES2_Grupo8.Models.Utente> Utentes { get; set; }
    }
}

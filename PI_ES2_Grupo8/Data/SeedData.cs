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
        }

        private static void SeedTroca(ServicoDomicilioDbContext db)
        {
            throw new NotImplementedException();
        }

        private static void SeedEnfermeiros(ServicoDomicilioDbContext db)
        {
            throw new NotImplementedException();
        }

    }
}

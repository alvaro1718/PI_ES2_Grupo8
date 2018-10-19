using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PI_ES2_Grupo8.Models;

namespace PI_ES2_Grupo8.Controllers
{
    public class EnfermeirosController : Controller
    {
        public IActionResult ViEnfermeiros()
        {
            return View();
        }

        public IActionResult ViEnfermeiros(Enfermeiros enfermeiro)
        {
    
            return View();
        }
    }
}
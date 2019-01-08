 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PI_ES2_Grupo8.Models;

namespace PI_ES2_Grupo8.Controllers
{
    [Authorize]
    public class EnfermeirosController : Controller
    {
        private const int PAGE_SIZE = 4;

        private readonly ServicoDomicilioDbContext _context;

        public EnfermeirosController(ServicoDomicilioDbContext context)
        {
            _context = context;
        }

        // GET: Enfermeiros
        public async Task<IActionResult> Index(EnfermeirosListViewModel model = null, int page = 1, string order = null)
        {
            string name = null;

            if (model != null)
            {
                name = model.CurrentName;
            }

            var enfermeiros = _context.Enfermeiros
                .Where(p => name == null || p.Nome.Contains(name));

            int numEnfermeiros = await enfermeiros.CountAsync();

            IEnumerable<Enfermeiros> enfermeirosList;

            if (order == "name")
            {
                enfermeirosList = await enfermeiros
                    .OrderBy(p => p.Nome)
                    .Skip(PAGE_SIZE * (page - 1))
                    .Take(PAGE_SIZE)
                    .ToListAsync();
            }
            else if (order == "especialização")
            {
                 enfermeirosList = await enfermeiros
                    .OrderBy(p => p.Especialização)
                    .Skip(PAGE_SIZE * (page - 1))
                    .Take(PAGE_SIZE)
                    .ToListAsync();
            }
            else
            {
                enfermeirosList = await enfermeiros
                    .OrderBy(p => p.Nome)
                    .Skip(PAGE_SIZE * (page - 1))
                    .Take(PAGE_SIZE)
                    .ToListAsync();
            }

            return View(
                new EnfermeirosListViewModel
                {
                    Enfermeiro = enfermeirosList,
                    Pagination = new PagingViewModel
                    {
                        CurrentPage = page,
                        PageSize = PAGE_SIZE,
                        Totaltems = numEnfermeiros,
                        Order = order
                    },
                    CurrentName = name,
                }
            );
        

        //var servicoDomicilioDbContext = _context.Enfermeiros.Include(e => e.Especialização);
        //return View(await servicoDomicilioDbContext.ToListAsync());
    }

        // GET: Enfermeiros/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enfermeiros = await _context.Enfermeiros
                .Include(e => e.Especialização)
                .FirstOrDefaultAsync(m => m.EnfermeirosId == id);
            if (enfermeiros == null)
            {
                return NotFound();
            }

            return View(enfermeiros);
        }

        // GET: Enfermeiros/Create
        [Authorize(Policy = "OnlyAdminAccess")]
        public IActionResult Create()
        {
            ViewData["EspecializaçãoId"] = new SelectList(_context.Especialização, "EspecializaçãoId", "Nome");
            return View();
        }

        // POST: Enfermeiros/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "OnlyAdminAccess")]
        public async Task<IActionResult> Create([Bind("EnfermeirosId,Nome,Telefone,Email,Morada,EspecializaçãoId")] Enfermeiros enfermeiros)
        {
            if (ModelState.IsValid)
            {
                //verificar Enfermeiro
                Enfermeiros verificarEnfermeiro = _context.Enfermeiros.SingleOrDefault(p => p.Nome == enfermeiros.Nome);

                if (verificarEnfermeiro == null)
                {
                    _context.Add(enfermeiros);
                    await _context.SaveChangesAsync();
                    return View("Enfermeiros", enfermeiros); // RedirectToAction(nameof(Index))
                }
                else
                {
                    ViewBag.Message = "Enfermeiro já existe!";
                    return View("Create");
                }

                //ViewData["EspecializaçãoId"] = new SelectList(_context.Especialização, "EspecializaçãoId", "Nome", enfermeiros.EspecializaçãoId);
            }
            return View(enfermeiros);
        }

        // GET: Enfermeiros/Edit/5
        [Authorize(Policy = "OnlyAdminAccess")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enfermeiros = await _context.Enfermeiros.FindAsync(id);
            if (enfermeiros == null)
            {
                return NotFound();
            }
            ViewData["EspecializaçãoId"] = new SelectList(_context.Especialização, "EspecializaçãoId", "Nome", enfermeiros.EspecializaçãoId);
            return View(enfermeiros);
        }

        // POST: Enfermeiros/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "OnlyAdminAccess")]
        public async Task<IActionResult> Edit(int id, [Bind("EnfermeirosId,Nome,Telefone,Email,Morada,EspecializaçãoId")] Enfermeiros enfermeiros)
        {
            if (id != enfermeiros.EnfermeirosId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(enfermeiros);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnfermeirosExists(enfermeiros.EnfermeirosId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["EspecializaçãoId"] = new SelectList(_context.Especialização, "EspecializaçãoId", "Nome", enfermeiros.EspecializaçãoId);
            return View(enfermeiros);
        }

        // GET: Enfermeiros/Delete/5
        [Authorize(Policy = "OnlyAdminAccess")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enfermeiros = await _context.Enfermeiros
                .Include(e => e.Especialização)
                .FirstOrDefaultAsync(m => m.EnfermeirosId == id);
            if (enfermeiros == null)
            {
                return NotFound();
            }

            return View(enfermeiros);
        }

        // POST: Enfermeiros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "OnlyAdminAccess")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var enfermeiros = await _context.Enfermeiros.FindAsync(id);
            _context.Enfermeiros.Remove(enfermeiros);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EnfermeirosExists(int id)
        {
            return _context.Enfermeiros.Any(e => e.EnfermeirosId == id);
        }
    }
}

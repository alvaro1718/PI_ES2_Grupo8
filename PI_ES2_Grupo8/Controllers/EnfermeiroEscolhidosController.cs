using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PI_ES2_Grupo8.Models;

namespace PI_ES2_Grupo8.Controllers
{
    public class EnfermeiroEscolhidosController : Controller
    {
        private readonly ServicoDomicilioDbContext _context;

        public EnfermeiroEscolhidosController(ServicoDomicilioDbContext context)
        {
            _context = context;
        }

        // GET: EnfermeiroEscolhidos
        public async Task<IActionResult> Index()
        {
            var servicoDomicilioDbContext = _context.EnfermeiroEscolhido.Include(e => e.Enfermeiros);
            return View(await servicoDomicilioDbContext.ToListAsync());
        }

        // GET: EnfermeiroEscolhidos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enfermeiroEscolhido = await _context.EnfermeiroEscolhido
                .Include(e => e.Enfermeiros)
                .FirstOrDefaultAsync(m => m.EnfermeiroEscolhidoId == id);
            if (enfermeiroEscolhido == null)
            {
                return NotFound();
            }

            return View(enfermeiroEscolhido);
        }

        // GET: EnfermeiroEscolhidos/Create
        public IActionResult Create()
        {
            ViewData["EnfermeirosId"] = new SelectList(_context.Enfermeiros, "EnfermeirosId", "Email");
            return View();
        }

        // POST: EnfermeiroEscolhidos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EnfermeiroEscolhidoId,EnfermeirosId")] EnfermeiroEscolhido enfermeiroEscolhido)
        {
            if (ModelState.IsValid)
            {
                _context.Add(enfermeiroEscolhido);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EnfermeirosId"] = new SelectList(_context.Enfermeiros, "EnfermeirosId", "Email", enfermeiroEscolhido.EnfermeirosId);
            return View(enfermeiroEscolhido);
        }

        // GET: EnfermeiroEscolhidos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enfermeiroEscolhido = await _context.EnfermeiroEscolhido.FindAsync(id);
            if (enfermeiroEscolhido == null)
            {
                return NotFound();
            }
            ViewData["EnfermeirosId"] = new SelectList(_context.Enfermeiros, "EnfermeirosId", "Email", enfermeiroEscolhido.EnfermeirosId);
            return View(enfermeiroEscolhido);
        }

        // POST: EnfermeiroEscolhidos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EnfermeiroEscolhidoId,EnfermeirosId")] EnfermeiroEscolhido enfermeiroEscolhido)
        {
            if (id != enfermeiroEscolhido.EnfermeiroEscolhidoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(enfermeiroEscolhido);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnfermeiroEscolhidoExists(enfermeiroEscolhido.EnfermeiroEscolhidoId))
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
            ViewData["EnfermeirosId"] = new SelectList(_context.Enfermeiros, "EnfermeirosId", "Email", enfermeiroEscolhido.EnfermeirosId);
            return View(enfermeiroEscolhido);
        }

        // GET: EnfermeiroEscolhidos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enfermeiroEscolhido = await _context.EnfermeiroEscolhido
                .Include(e => e.Enfermeiros)
                .FirstOrDefaultAsync(m => m.EnfermeiroEscolhidoId == id);
            if (enfermeiroEscolhido == null)
            {
                return NotFound();
            }

            return View(enfermeiroEscolhido);
        }

        // POST: EnfermeiroEscolhidos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var enfermeiroEscolhido = await _context.EnfermeiroEscolhido.FindAsync(id);
            _context.EnfermeiroEscolhido.Remove(enfermeiroEscolhido);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EnfermeiroEscolhidoExists(int id)
        {
            return _context.EnfermeiroEscolhido.Any(e => e.EnfermeiroEscolhidoId == id);
        }
    }
}

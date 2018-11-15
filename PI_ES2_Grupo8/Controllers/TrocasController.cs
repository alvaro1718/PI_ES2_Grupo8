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
    public class TrocasController : Controller
    {
        private readonly ServicoDomicilioDbContext _context;

        public TrocasController(ServicoDomicilioDbContext context)
        {
            _context = context;
        }

        // GET: Trocas
        public async Task<IActionResult> Index()
        {
            var servicoDomicilioDbContext = _context.Troca.Include(t => t.Enfermeiros).Include(t => t.HorarioServicoDomicilio);
            return View(await servicoDomicilioDbContext.ToListAsync());
        }

        // GET: Trocas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var troca = await _context.Troca
                .Include(t => t.Enfermeiros)
                .Include(t => t.HorarioServicoDomicilio)
                .FirstOrDefaultAsync(m => m.TrocaId == id);
            if (troca == null)
            {
                return NotFound();
            }

            return View(troca);
        }

        // GET: Trocas/Create
        public IActionResult Create()
        {
            ViewData["EnfermeirosId"] = new SelectList(_context.Enfermeiros, "EnfermeirosId", "Email");
            ViewData["HorarioServicoDomicilioId"] = new SelectList(_context.HorarioServicoDomicilio, "HorarioServicoDomicilioId", "HorarioServicoDomicilioId");
            return View();
        }

        // POST: Trocas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TrocaId,justificacao,EnfermeirosId,HorarioServicoDomicilioId")] Troca troca)
        {
            if (ModelState.IsValid)
            {
                _context.Add(troca);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EnfermeirosId"] = new SelectList(_context.Enfermeiros, "EnfermeirosId", "Email", troca.EnfermeirosId);
            ViewData["HorarioServicoDomicilioId"] = new SelectList(_context.HorarioServicoDomicilio, "HorarioServicoDomicilioId", "HorarioServicoDomicilioId", troca.HorarioServicoDomicilioId);
            return View(troca);
        }

        // GET: Trocas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var troca = await _context.Troca.FindAsync(id);
            if (troca == null)
            {
                return NotFound();
            }
            ViewData["EnfermeirosId"] = new SelectList(_context.Enfermeiros, "EnfermeirosId", "Email", troca.EnfermeirosId);
            ViewData["HorarioServicoDomicilioId"] = new SelectList(_context.HorarioServicoDomicilio, "HorarioServicoDomicilioId", "HorarioServicoDomicilioId", troca.HorarioServicoDomicilioId);
            return View(troca);
        }

        // POST: Trocas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TrocaId,justificacao,EnfermeirosId,HorarioServicoDomicilioId")] Troca troca)
        {
            if (id != troca.TrocaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(troca);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrocaExists(troca.TrocaId))
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
            ViewData["EnfermeirosId"] = new SelectList(_context.Enfermeiros, "EnfermeirosId", "Email", troca.EnfermeirosId);
            ViewData["HorarioServicoDomicilioId"] = new SelectList(_context.HorarioServicoDomicilio, "HorarioServicoDomicilioId", "HorarioServicoDomicilioId", troca.HorarioServicoDomicilioId);
            return View(troca);
        }

        // GET: Trocas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var troca = await _context.Troca
                .Include(t => t.Enfermeiros)
                .Include(t => t.HorarioServicoDomicilio)
                .FirstOrDefaultAsync(m => m.TrocaId == id);
            if (troca == null)
            {
                return NotFound();
            }

            return View(troca);
        }

        // POST: Trocas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var troca = await _context.Troca.FindAsync(id);
            _context.Troca.Remove(troca);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrocaExists(int id)
        {
            return _context.Troca.Any(e => e.TrocaId == id);
        }
    }
}

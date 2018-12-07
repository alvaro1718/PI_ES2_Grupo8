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
    public class EnfermeiroRequerenteController : Controller
    {
        private readonly ServicoDomicilioDbContext _context;

        public EnfermeiroRequerenteController(ServicoDomicilioDbContext context)
        {
            _context = context;
        }

        // GET: EnfermeiroRequerente
        public async Task<IActionResult> Index()
        {
            var servicoDomicilioDbContext = _context.EnfermeiroRequerente.Include(e => e.Enfermeiros);
            return View(await servicoDomicilioDbContext.ToListAsync());
        }

        // GET: EnfermeiroRequerente/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enfermeiroRequerente = await _context.EnfermeiroRequerente
                .Include(e => e.Enfermeiros)
                .FirstOrDefaultAsync(m => m.EnfermeiroRequerenteId == id);
            if (enfermeiroRequerente == null)
            {
                return NotFound();
            }

            return View(enfermeiroRequerente);
        }

        // GET: EnfermeiroRequerente/Create
        public IActionResult Create()
        {
            ViewData["EnfermeirosId"] = new SelectList(_context.Enfermeiros, "EnfermeirosId", "Nome");
            return View();
        }

        // POST: EnfermeiroRequerente/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EnfermeiroRequerenteId,EnfermeirosId")] EnfermeiroRequerente enfermeiroRequerente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(enfermeiroRequerente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EnfermeirosId"] = new SelectList(_context.Enfermeiros, "EnfermeirosId", "Nome", enfermeiroRequerente.EnfermeirosId);
            return View(enfermeiroRequerente);
        }

        // GET: EnfermeiroRequerente/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enfermeiroRequerente = await _context.EnfermeiroRequerente.FindAsync(id);
            if (enfermeiroRequerente == null)
            {
                return NotFound();
            }
            ViewData["EnfermeirosId"] = new SelectList(_context.Enfermeiros, "EnfermeirosId", "Nome", enfermeiroRequerente.EnfermeirosId);
            return View(enfermeiroRequerente);
        }

        // POST: EnfermeiroRequerente/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EnfermeiroRequerenteId,EnfermeirosId")] EnfermeiroRequerente enfermeiroRequerente)
        {
            if (id != enfermeiroRequerente.EnfermeiroRequerenteId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(enfermeiroRequerente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnfermeiroRequerenteExists(enfermeiroRequerente.EnfermeiroRequerenteId))
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
            ViewData["EnfermeirosId"] = new SelectList(_context.Enfermeiros, "EnfermeirosId", "Nome", enfermeiroRequerente.EnfermeirosId);
            return View(enfermeiroRequerente);
        }

        // GET: EnfermeiroRequerente/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enfermeiroRequerente = await _context.EnfermeiroRequerente
                .Include(e => e.Enfermeiros)
                .FirstOrDefaultAsync(m => m.EnfermeiroRequerenteId == id);
            if (enfermeiroRequerente == null)
            {
                return NotFound();
            }

            return View(enfermeiroRequerente);
        }

        // POST: EnfermeiroRequerente/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var enfermeiroRequerente = await _context.EnfermeiroRequerente.FindAsync(id);
            _context.EnfermeiroRequerente.Remove(enfermeiroRequerente);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EnfermeiroRequerenteExists(int id)
        {
            return _context.EnfermeiroRequerente.Any(e => e.EnfermeiroRequerenteId == id);
        }
    }
}

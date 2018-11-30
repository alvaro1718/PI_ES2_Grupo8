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
    public class HorarioServicoDomicilioController : Controller
    {
        private readonly ServicoDomicilioDbContext _context;

        public HorarioServicoDomicilioController(ServicoDomicilioDbContext context)
        {
            _context = context;
        }

        // GET: HorarioServicoDomicilio
        public async Task<IActionResult> Index()
        {
            var servicoDomicilioDbContext = _context.HorarioServicoDomicilio.Include(h => h.Enfermeiros);
            return View(await servicoDomicilioDbContext.ToListAsync());
        }

        // GET: HorarioServicoDomicilio/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var horarioServicoDomicilio = await _context.HorarioServicoDomicilio
                .Include(h => h.Enfermeiros)
                .FirstOrDefaultAsync(m => m.HorarioServicoDomicilioId == id);
            if (horarioServicoDomicilio == null)
            {
                return NotFound();
            }

            return View(horarioServicoDomicilio);
        }

        // GET: HorarioServicoDomicilio/Create
        public IActionResult Create()
        {
            ViewData["EnfermeirosId"] = new SelectList(_context.Enfermeiros, "EnfermeirosId", "Email");
            return View();
        }

        // POST: HorarioServicoDomicilio/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HorarioServicoDomicilioId,Data,HoraInicio,HoraFim,EnfermeirosId")] HorarioServicoDomicilio horarioServicoDomicilio)
        {
            if (ModelState.IsValid)
            {
                _context.Add(horarioServicoDomicilio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EnfermeirosId"] = new SelectList(_context.Enfermeiros, "EnfermeirosId", "Email", horarioServicoDomicilio.EnfermeirosId);
            return View(horarioServicoDomicilio);
        }

        // GET: HorarioServicoDomicilio/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var horarioServicoDomicilio = await _context.HorarioServicoDomicilio.FindAsync(id);
            if (horarioServicoDomicilio == null)
            {
                return NotFound();
            }
            ViewData["EnfermeirosId"] = new SelectList(_context.Enfermeiros, "EnfermeirosId", "Email", horarioServicoDomicilio.EnfermeirosId);
            return View(horarioServicoDomicilio);
        }

        // POST: HorarioServicoDomicilio/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HorarioServicoDomicilioId,Data,HoraInicio,HoraFim,EnfermeirosId")] HorarioServicoDomicilio horarioServicoDomicilio)
        {
            if (id != horarioServicoDomicilio.HorarioServicoDomicilioId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(horarioServicoDomicilio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HorarioServicoDomicilioExists(horarioServicoDomicilio.HorarioServicoDomicilioId))
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
            ViewData["EnfermeirosId"] = new SelectList(_context.Enfermeiros, "EnfermeirosId", "Email", horarioServicoDomicilio.EnfermeirosId);
            return View(horarioServicoDomicilio);
        }

        // GET: HorarioServicoDomicilio/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var horarioServicoDomicilio = await _context.HorarioServicoDomicilio
                .Include(h => h.Enfermeiros)
                .FirstOrDefaultAsync(m => m.HorarioServicoDomicilioId == id);
            if (horarioServicoDomicilio == null)
            {
                return NotFound();
            }

            return View(horarioServicoDomicilio);
        }

        // POST: HorarioServicoDomicilio/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var horarioServicoDomicilio = await _context.HorarioServicoDomicilio.FindAsync(id);
            _context.HorarioServicoDomicilio.Remove(horarioServicoDomicilio);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HorarioServicoDomicilioExists(int id)
        {
            return _context.HorarioServicoDomicilio.Any(e => e.HorarioServicoDomicilioId == id);
        }
    }
}

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
    public class HorarioTrabalhoController : Controller
    {
        private readonly ServicoDomicilioDbContext _context;

        public HorarioTrabalhoController(ServicoDomicilioDbContext context)
        {
            _context = context;
        }

        // GET: HorarioTrabalho
        public async Task<IActionResult> Index()
        {
            var servicoDomicilioDbContext = _context.HorarioTrabalho.Include(h => h.Enfermeiros);
            return View(await servicoDomicilioDbContext.ToListAsync());
        }

        // GET: HorarioTrabalho/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var horarioTrabalho = await _context.HorarioTrabalho
                .Include(h => h.Enfermeiros)
                .FirstOrDefaultAsync(m => m.HorarioTrabalhoId == id);
            if (horarioTrabalho == null)
            {
                return NotFound();
            }

            return View(horarioTrabalho);
        }

        // GET: HorarioTrabalho/Create
        public IActionResult Create()
        {
            ViewData["EnfermeirosId"] = new SelectList(_context.Enfermeiros, "EnfermeirosId", "Email");
            return View();
        }

        // POST: HorarioTrabalho/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HorarioTrabalhoId,Data,HoraInicio,HoraFim,EnfermeirosId,Troca")] HorarioTrabalho horarioTrabalho)
        {
            if (ModelState.IsValid)
            {
                _context.Add(horarioTrabalho);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EnfermeirosId"] = new SelectList(_context.Enfermeiros, "EnfermeirosId", "Email", horarioTrabalho.EnfermeirosId);
            return View(horarioTrabalho);
        }

        // GET: HorarioTrabalho/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var horarioTrabalho = await _context.HorarioTrabalho.FindAsync(id);
            if (horarioTrabalho == null)
            {
                return NotFound();
            }
            ViewData["EnfermeirosId"] = new SelectList(_context.Enfermeiros, "EnfermeirosId", "Email", horarioTrabalho.EnfermeirosId);
            return View(horarioTrabalho);
        }

        // POST: HorarioTrabalho/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HorarioTrabalhoId,Data,HoraInicio,HoraFim,EnfermeirosId,Troca")] HorarioTrabalho horarioTrabalho)
        {
            if (id != horarioTrabalho.HorarioTrabalhoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(horarioTrabalho);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HorarioTrabalhoExists(horarioTrabalho.HorarioTrabalhoId))
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
            ViewData["EnfermeirosId"] = new SelectList(_context.Enfermeiros, "EnfermeirosId", "Email", horarioTrabalho.EnfermeirosId);
            return View(horarioTrabalho);
        }

        // GET: HorarioTrabalho/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var horarioTrabalho = await _context.HorarioTrabalho
                .Include(h => h.Enfermeiros)
                .FirstOrDefaultAsync(m => m.HorarioTrabalhoId == id);
            if (horarioTrabalho == null)
            {
                return NotFound();
            }

            return View(horarioTrabalho);
        }

        // POST: HorarioTrabalho/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var horarioTrabalho = await _context.HorarioTrabalho.FindAsync(id);
            _context.HorarioTrabalho.Remove(horarioTrabalho);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HorarioTrabalhoExists(int id)
        {
            return _context.HorarioTrabalho.Any(e => e.HorarioTrabalhoId == id);
        }
    }
}

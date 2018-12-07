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
    public class PedidoTrocasController : Controller
    {
        private readonly ServicoDomicilioDbContext _context;

        public PedidoTrocasController(ServicoDomicilioDbContext context)
        {
            _context = context;
        }

        // GET: PedidoTrocas
        public async Task<IActionResult> Index()
        {
            var servicoDomicilioDbContext = _context.Troca.Include(t => t.EnfermeiroRequerente).Include(t => t.HorarioTrabalhoNovo);
            return View(await servicoDomicilioDbContext.ToListAsync());
        }

        // GET: PedidoTrocas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var troca = await _context.Troca
                .Include(t => t.EnfermeiroRequerente)
                .Include(t => t.HorarioTrabalhoNovo)
                .FirstOrDefaultAsync(m => m.TrocaId == id);
            if (troca == null)
            {
                return NotFound();
            }

            return View(troca);
        }

        // GET: PedidoTrocas/Create
        public IActionResult Create()
        {
            ViewData["EnfermeirosId"] = new SelectList(_context.Enfermeiros, "EnfermeirosId", "Email");
            ViewData["HorarioTrabalhoId"] = new SelectList(_context.HorarioTrabalho, "HorarioTrabalhoId", "HorarioTrabalhoId");
            return View();
        }

        // POST: PedidoTrocas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TrocaId,Justificação,EnfermeirosId,Data,HorarioTrabalhoId")] Troca troca)
        {
            if (ModelState.IsValid)
            {
                _context.Add(troca);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EnfermeirosId"] = new SelectList(_context.Enfermeiros, "EnfermeirosId", "Email", troca.EnfermeirosId);
            ViewData["HorarioTrabalhoId"] = new SelectList(_context.HorarioTrabalho, "HorarioTrabalhoId", "HorarioTrabalhoId", troca.HorarioTrabalhoId);
            return View(troca);
        }

        // GET: PedidoTrocas/Edit/5
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
            ViewData["HorarioTrabalhoId"] = new SelectList(_context.HorarioTrabalho, "HorarioTrabalhoId", "HorarioTrabalhoId", troca.HorarioTrabalhoId);
            return View(troca);
        }

        // POST: PedidoTrocas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TrocaId,Justificação,EnfermeirosId,Data,HorarioTrabalhoId")] Troca troca)
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
            ViewData["HorarioTrabalhoId"] = new SelectList(_context.HorarioTrabalho, "HorarioTrabalhoId", "HorarioTrabalhoId", troca.HorarioTrabalhoId);
            return View(troca);
        }

        // GET: PedidoTrocas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var troca = await _context.Troca
                .Include(t => t.EnfermeiroRequerente)
                .Include(t => t.HorarioTrabalhoNovo)
                .FirstOrDefaultAsync(m => m.TrocaId == id);
            if (troca == null)
            {
                return NotFound();
            }

            return View(troca);
        }

        // POST: PedidoTrocas/Delete/5
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

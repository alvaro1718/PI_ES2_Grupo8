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
    public class GHorariosController : Controller
    {
        private readonly ServicoDomicilioDbContext _context;

        public GHorariosController(ServicoDomicilioDbContext context)
        {
            _context = context;
        }

        // GET: GHorarios
        public async Task<IActionResult> Index()
        {
            return View(await _context.gerarHorarios.ToListAsync());
        }

        // GET: GHorarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gerarHorarios = await _context.gerarHorarios
                .FirstOrDefaultAsync(m => m.gerarHorariosId == id);
            if (gerarHorarios == null)
            {
                return NotFound();
            }

            return View(gerarHorarios);
        }

        // GET: GHorarios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GHorarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("gerarHorariosId,Data,Hora,Enfermeiros,Utente")] gerarHorarios gerarHorarios)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gerarHorarios);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gerarHorarios);
        }

        // GET: GHorarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gerarHorarios = await _context.gerarHorarios.FindAsync(id);
            if (gerarHorarios == null)
            {
                return NotFound();
            }
            return View(gerarHorarios);
        }

        // POST: GHorarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("gerarHorariosId,Data,Hora,Enfermeiros,Utente")] gerarHorarios gerarHorarios)
        {
            if (id != gerarHorarios.gerarHorariosId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gerarHorarios);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!gerarHorariosExists(gerarHorarios.gerarHorariosId))
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
            return View(gerarHorarios);
        }

        // GET: GHorarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gerarHorarios = await _context.gerarHorarios
                .FirstOrDefaultAsync(m => m.gerarHorariosId == id);
            if (gerarHorarios == null)
            {
                return NotFound();
            }

            return View(gerarHorarios);
        }

        // POST: GHorarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gerarHorarios = await _context.gerarHorarios.FindAsync(id);
            _context.gerarHorarios.Remove(gerarHorarios);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool gerarHorariosExists(int id)
        {
            return _context.gerarHorarios.Any(e => e.gerarHorariosId == id);
        }
    }
}

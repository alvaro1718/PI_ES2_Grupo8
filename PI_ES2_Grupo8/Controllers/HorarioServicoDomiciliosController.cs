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
    public class HorarioServicoDomiciliosController : Controller
    {
        private readonly ServicoDomicilioDbContext _context;

        public HorarioServicoDomiciliosController(ServicoDomicilioDbContext context)
        {
            _context = context;
        }

        // GET: HorarioServicoDomicilios
        public async Task<IActionResult> Index()
        {
            return View(await _context.HorarioServicoDomicilio.ToListAsync());
        }

        // GET: HorarioServicoDomicilios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var horarioServicoDomicilio = await _context.HorarioServicoDomicilio
                .FirstOrDefaultAsync(m => m.HorarioServicoDomicilioId == id);
            if (horarioServicoDomicilio == null)
            {
                return NotFound();
            }

            return View(horarioServicoDomicilio);
        }

        // GET: HorarioServicoDomicilios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HorarioServicoDomicilios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HorarioServicoDomicilioId,Data,Hora,Enfermeiros,Utente,Tratamento")] HorarioServicoDomicilio horarioServicoDomicilio)
        {
            if (ModelState.IsValid)
            {
                _context.Add(horarioServicoDomicilio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(horarioServicoDomicilio);
        }

        // GET: HorarioServicoDomicilios/Edit/5
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
            return View(horarioServicoDomicilio);
        }

        // POST: HorarioServicoDomicilios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HorarioServicoDomicilioId,Data,Hora,Enfermeiros,Utente,Tratamento")] HorarioServicoDomicilio horarioServicoDomicilio)
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
            return View(horarioServicoDomicilio);
        }

        // GET: HorarioServicoDomicilios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var horarioServicoDomicilio = await _context.HorarioServicoDomicilio
                .FirstOrDefaultAsync(m => m.HorarioServicoDomicilioId == id);
            if (horarioServicoDomicilio == null)
            {
                return NotFound();
            }

            return View(horarioServicoDomicilio);
        }

        // POST: HorarioServicoDomicilios/Delete/5
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

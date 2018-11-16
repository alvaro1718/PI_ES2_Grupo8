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
    public class EspecializaçãoController : Controller
    {
        private readonly ServicoDomicilioDbContext _context;

        public EspecializaçãoController(ServicoDomicilioDbContext context)
        {
            _context = context;
        }

        // GET: Especialização
        public async Task<IActionResult> Index()
        {
            return View(await _context.Especialização.ToListAsync());
        }

        // GET: Especialização/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var especialização = await _context.Especialização
                .FirstOrDefaultAsync(m => m.EspecializaçãoId == id);
            if (especialização == null)
            {
                return NotFound();
            }

            return View(especialização);
        }

        // GET: Especialização/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Especialização/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EspecializaçãoId,Nome")] Especialização especialização)
        {
            if (ModelState.IsValid)
            {
                _context.Add(especialização);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(especialização);
        }

        // GET: Especialização/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var especialização = await _context.Especialização.FindAsync(id);
            if (especialização == null)
            {
                return NotFound();
            }
            return View(especialização);
        }

        // POST: Especialização/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EspecializaçãoId,Nome")] Especialização especialização)
        {
            if (id != especialização.EspecializaçãoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(especialização);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EspecializaçãoExists(especialização.EspecializaçãoId))
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
            return View(especialização);
        }

        // GET: Especialização/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var especialização = await _context.Especialização
                .FirstOrDefaultAsync(m => m.EspecializaçãoId == id);
            if (especialização == null)
            {
                return NotFound();
            }

            return View(especialização);
        }

        // POST: Especialização/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var especialização = await _context.Especialização.FindAsync(id);
            _context.Especialização.Remove(especialização);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EspecializaçãoExists(int id)
        {
            return _context.Especialização.Any(e => e.EspecializaçãoId == id);
        }
    }
}

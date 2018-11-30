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
    public class ReceitasController : Controller
    {
        private readonly ServicoDomicilioDbContext _context;

        public ReceitasController(ServicoDomicilioDbContext context)
        {
            _context = context;
        }

        // GET: Receitas
        public async Task<IActionResult> Index()
        {
            var servicoDomicilioDbContext = _context.Receita.Include(r => r.medico).Include(r => r.utente);
            return View(await servicoDomicilioDbContext.ToListAsync());
        }

        // GET: Receitas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var receita = await _context.Receita
                .Include(r => r.medico)
                .Include(r => r.utente)
                .FirstOrDefaultAsync(m => m.ReceitaId == id);
            if (receita == null)
            {
                return NotFound();
            }

            return View(receita);
        }

        // GET: Receitas/Create
        public IActionResult Create()
        {
            ViewData["MedicoId"] = new SelectList(_context.Medico, "MedicoId", "Nome");
            ViewData["UtenteId"] = new SelectList(_context.Utente, "UtenteId", "Nome");
            return View();
        }

        // POST: Receitas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReceitaId,MedicoId,UtenteId,Date,Nreceita")] Receita receita)
        {
            DateTime date = DateTime.Now;
            if (ModelState.IsValid)
            {
                receita.Date = date;
                int ultimareceita = _context.Receita.Max(p=>p.ReceitaId);
                receita.Nreceita = ultimareceita+1;
                _context.Add(receita);
                await _context.SaveChangesAsync();
                return RedirectToAction("Create", "ReceitarTratamentos",receita);//RedirectToAction(nameof(Index));
            }
            ViewData["MedicoId"] = new SelectList(_context.Medico, "MedicoId", "Nome", receita.MedicoId);
            ViewData["UtenteId"] = new SelectList(_context.Utente, "UtenteId", "Nome", receita.UtenteId);
            return View(receita);
        }

        // GET: Receitas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var receita = await _context.Receita.FindAsync(id);
            if (receita == null)
            {
                return NotFound();
            }
            ViewData["MedicoId"] = new SelectList(_context.Medico, "MedicoId", "Nome", receita.MedicoId);
            ViewData["UtenteId"] = new SelectList(_context.Utente, "UtenteId", "Nome", receita.UtenteId);
            return View(receita);
        }

        // POST: Receitas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReceitaId,MedicoId,UtenteId,Date,Nreceita")] Receita receita)
        {
            if (id != receita.ReceitaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(receita);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReceitaExists(receita.ReceitaId))
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
            ViewData["MedicoId"] = new SelectList(_context.Medico, "MedicoId", "Nome", receita.MedicoId);
            ViewData["UtenteId"] = new SelectList(_context.Utente, "UtenteId", "Nome", receita.UtenteId);
            return View(receita);
        }

        // GET: Receitas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var receita = await _context.Receita
                .Include(r => r.medico)
                .Include(r => r.utente)
                .FirstOrDefaultAsync(m => m.ReceitaId == id);
            if (receita == null)
            {
                return NotFound();
            }

            return View(receita);
        }

        // POST: Receitas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var receita = await _context.Receita.FindAsync(id);
            _context.Receita.Remove(receita);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReceitaExists(int id)
        {
            return _context.Receita.Any(e => e.ReceitaId == id);
        }
    }
}

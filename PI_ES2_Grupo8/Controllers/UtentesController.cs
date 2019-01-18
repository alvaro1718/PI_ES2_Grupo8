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
    public class UtentesController : Controller
    {
        private const int PAGE_SIZE = 4;
        private readonly ServicoDomicilioDbContext _context;

        public UtentesController(ServicoDomicilioDbContext context)
        {
            _context = context;
        }

        // GET: Utentes
        public async Task<IActionResult> Index(UtentesListViewModel model = null, int page = 1)
        {

            string nome = null;

            if (model != null)
            {
                nome = model.CurrentName;
                //page = 1;
            }

            var utentes = _context.Utente
                .Where(p =>nome == null || p.Nome.Contains(nome));

            int numUtentes = await utentes.CountAsync();

            if (page > (numUtentes / PAGE_SIZE) + 1)
            {
                page = 1;
            }

            var UtentesList = await utentes
                    .OrderBy(p => p.Nome)
                    .Skip(PAGE_SIZE * (page - 1))
                    .Take(PAGE_SIZE)
                    .ToListAsync();

            return View(
                new UtentesListViewModel
                {
                    Utentes = UtentesList,
                    Pagination = new PagingViewModel
                    {
                        CurrentPage = page,
                        PageSize = PAGE_SIZE,
                        Totaltems = numUtentes
                    },
                    CurrentName = nome
                }
            );

            //return View(await _context.Utente.ToListAsync());
        }

        // GET: Utentes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var utente = await _context.Utente
                .FirstOrDefaultAsync(m => m.UtenteId == id);
            if (utente == null)
            {
                return NotFound();
            }

            return View(utente);
        }

        // GET: Utentes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Utentes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UtenteId,Nome,N_Utente_Saude,Morada,Telefone,Email,Problemas")] Utente utente)
        {
           
            if (ModelState.IsValid)
            {
                Utente verificarUtente = _context.Utente.SingleOrDefault(p => p.N_Utente_Saude == utente.N_Utente_Saude);

                if (verificarUtente == null)
                {
                    _context.Add(utente);
                    await _context.SaveChangesAsync();
                    ViewBag.Message = "UtenteCriado";
                    return View("Details", utente);//return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Message = "Utente já existente.";
                    return View("Create");
                }
            }
            return View(utente);
        }

        // GET: Utentes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var utente = await _context.Utente.FindAsync(id);
            if (utente == null)
            {
                return NotFound();
            }
            return View(utente);
        }

        // POST: Utentes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UtenteId,Nome,N_Utente_Saude,Morada,Telefone,Email,Problemas")] Utente utente)
        {
            if (id != utente.UtenteId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(utente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UtenteExists(utente.UtenteId))
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
            return View(utente);
        }

        // GET: Utentes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var utente = await _context.Utente
                .FirstOrDefaultAsync(m => m.UtenteId == id);
            if (utente == null)
            {
                return NotFound();
            }

            return View(utente);
        }

        // POST: Utentes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var utente = await _context.Utente.FindAsync(id);
            _context.Utente.Remove(utente);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [ActionName("Confirmar")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Medico.ToListAsync());
        }
        private bool UtenteExists(int id)
        {
            return _context.Utente.Any(e => e.UtenteId == id);
        }
    }
}

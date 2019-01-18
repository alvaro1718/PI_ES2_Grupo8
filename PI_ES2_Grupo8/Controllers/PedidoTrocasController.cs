using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PI_ES2_Grupo8.Models;

namespace PI_ES2_Grupo8.Controllers
{
    [Authorize]
    public class PedidoTrocasController : Controller
    {
        private const int PAGE_SIZE = 3;
        private readonly ServicoDomicilioDbContext _context;

        public PedidoTrocasController(ServicoDomicilioDbContext context)
        {
            _context = context;
        }

        // GET: PedidoTrocas
        public async Task<IActionResult> Index(PedidosTrocasListViewModel model = null, int page = 1, string order = null)
        {

            string name = null;

            if (model != null)
            {
                name = model.CurrentName;
            }

            var pedidoTrocas = _context.Troca
                .Where(p => name == null || p.EnfermeiroRequerente.Nome.Contains(name))
                .Include(p => p.EnfermeiroRequerente)
                .Include(p => p.EnfermeiroEscolhido)
                .Include(p => p.HorarioTrabalhoAntigo)
                .Include(p => p.HorarioTrabalhoNovo); ;


            int numPedidos = await pedidoTrocas.CountAsync();

            IEnumerable<Troca> pedidosList;

            if (order == "name")
            {
                pedidosList = await pedidoTrocas
                    .OrderBy(p => p.EnfermeiroRequerente.Nome)
                    .Skip(PAGE_SIZE * (page - 1))
                    .Take(PAGE_SIZE)
                    .ToListAsync();
            }
            else if (order == "data")
            {
                pedidosList = await pedidoTrocas
                    .OrderBy(p => p.Data)
                    .Skip(PAGE_SIZE * (page - 1))
                    .Take(PAGE_SIZE)
                    .ToListAsync();
            }
            else
            {
                pedidosList = await pedidoTrocas
                   .OrderBy(p => p.HorarioTrabalhoAntigo.HoraInicio)
                   .Skip(PAGE_SIZE * (page - 1))
                   .Take(PAGE_SIZE)
                   .ToListAsync();
            }

            return View(
                new PedidosTrocasListViewModel
                {
                    PedidosTrocas = pedidosList,
                    Pagination = new PagingViewModel
                    {
                        CurrentPage = page,
                        PageSize = PAGE_SIZE,
                        Totaltems = numPedidos,
                        Order = order
                    },
                    CurrentName = name,
                }
            );


            //var servicoDomicilioDbContext = _context.Troca.Include(t => t.EnfermeiroRequerente).Include(t => t.EnfermeiroEscolhido).Include(t => t.HorarioTrabalhoAntigo).Include(t => t.HorarioTrabalhoNovo);
            //return View(await servicoDomicilioDbContext.ToListAsync());
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
                .Include(t => t.EnfermeiroEscolhido) //
                .Include(t => t.HorarioTrabalhoAntigo)
                .Include(t => t.HorarioTrabalhoNovo)
                .FirstOrDefaultAsync(m => m.TrocaId == id);
            if (troca == null)
            {
                return NotFound();
            }

            return View(troca);
        }

        // GET: PedidoTrocas/Create
        //[Authorize(Policy = "OnlyAdminAccess")]
        public IActionResult Create()
        {
            ViewData["EnfermeirosId"] = new SelectList(_context.Enfermeiros, "EnfermeirosId", "Nome");
            ViewData["EnfermeirosEId"] = new SelectList(_context.Enfermeiros, "EnfermeirosId", "Nome");//
            ViewData["HorarioTrabalhoAntigoId"] = new SelectList(_context.HorarioTrabalho, "HorarioTrabalhoId", "HoraInicio");
            ViewData["HorarioTrabalhoId"] = new SelectList(_context.HorarioTrabalho, "HorarioTrabalhoId", "HoraInicio");
            return View();
        }

        // POST: PedidoTrocas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Policy = "OnlyAdminAccess")]
        public async Task<IActionResult> Create([Bind("TrocaId,Justificação,EnfermeirosId,EnfermeirosEId,Data,HorarioTrabalhoId,HorarioTrabalhoAntigoId,Aprovar")] Troca troca)
        {
            if (ModelState.IsValid)
            {
                _context.Add(troca);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EnfermeirosId"] = new SelectList(_context.Enfermeiros, "EnfermeirosId", "Nome", troca.EnfermeirosId);
            ViewData["EnfermeirosEId"] = new SelectList(_context.Enfermeiros, "EnfermeirosId", "Nome", troca.EnfermeirosEId);//
            ViewData["HorarioTrabalhoAntigoId"] = new SelectList(_context.HorarioTrabalho, "HorarioTrabalhoId", "HorarioTrabalhoId", troca.HorarioTrabalhoAntigoId);
            ViewData["HorarioTrabalhoId"] = new SelectList(_context.HorarioTrabalho, "HorarioTrabalhoId", "HorarioTrabalhoId", troca.HorarioTrabalhoId);
            return View(troca);
        }

        // GET: PedidoTrocas/Edit/5
        [Authorize(Policy = "OnlyAdminAccess")]
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
            ViewData["EnfermeirosId"] = new SelectList(_context.Enfermeiros, "EnfermeirosId", "Nome", troca.EnfermeirosId);
            ViewData["EnfermeirosEId"] = new SelectList(_context.Enfermeiros, "EnfermeirosId", "Nome", troca.EnfermeirosEId);
            ViewData["HorarioTrabalhoAntigoId"] = new SelectList(_context.HorarioTrabalho, "HorarioTrabalhoId", "HorarioTrabalhoId", troca.HorarioTrabalhoAntigoId);
            ViewData["HorarioTrabalhoId"] = new SelectList(_context.HorarioTrabalho, "HorarioTrabalhoId", "HorarioTrabalhoId", troca.HorarioTrabalhoId);
            return View(troca);
        }

        // POST: PedidoTrocas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "OnlyAdminAccess")]
        public async Task<IActionResult> Edit(int id, [Bind("TrocaId,Justificação,EnfermeirosId,EnfermeirosEId,Data,HorarioTrabalhoId,HorarioTrabalhoAntigoId,Aprovar")] Troca troca)
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
            ViewData["EnfermeirosId"] = new SelectList(_context.Enfermeiros, "EnfermeirosId", "Nome", troca.EnfermeirosId);
            ViewData["EnfermeirosEId"] = new SelectList(_context.Enfermeiros, "EnfermeirosId", "Nome", troca.EnfermeirosEId);
            ViewData["HorarioTrabalhoAntigoId"] = new SelectList(_context.HorarioTrabalho, "HorarioTrabalhoId", "HorarioTrabalhoId", troca.HorarioTrabalhoAntigoId);
            ViewData["HorarioTrabalhoId"] = new SelectList(_context.HorarioTrabalho, "HorarioTrabalhoId", "HorarioTrabalhoId", troca.HorarioTrabalhoId);
            return View(troca);
        }

        // GET: PedidoTrocas/Delete/5
        //[Authorize(Policy = "OnlyAdminAccess")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var troca = await _context.Troca
                .Include(t => t.EnfermeiroRequerente)
                .Include(t => t.EnfermeiroEscolhido)
                .Include(t => t.HorarioTrabalhoAntigo)
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
        //[Authorize(Policy = "OnlyAdminAccess")]
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

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
    public class TratamentosController : Controller
    {
        private readonly ServicoDomicilioDbContext _context;

        public TratamentosController(ServicoDomicilioDbContext context)
        {
            _context = context;
        }

        // GET: Tratamentos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Tratamento.ToListAsync());
        }

        // GET: Tratamentos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tratamento = await _context.Tratamento
                .FirstOrDefaultAsync(m => m.TratamentoId == id);
            if (tratamento == null)
            {
                return NotFound();
            }

            return View(tratamento);
        }

        // GET: Tratamentos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tratamentos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TratamentoId,TipodeTratamento")] Tratamento tratamento)
        {
            if (ModelState.IsValid)
            {
                verificarTratamento = _context.Tratamento.SingleOrDefault(p => p.TipodeTratamento == tratamento.TipodeTratamento);
                //  if (tratamento.TipodeTratamento==_context.Tratamento.(p =>p.TipodeTratamento.Contains(tratamento.TipodeTratamento)) ) { }
                if (verificarTratamento == null)
                {
                    _context.Add(tratamento);
                    await _context.SaveChangesAsync();
                    return View("TratamentoCriado", tratamento);//RedirectToAction(nameof(Index));
                }
                else {
                         ViewBag.Message = "Tratamento já existente.";
                    return View("Create");
                }
            }
            return View(tratamento);
        }
        public static String TratamentoSelecionado;
        // GET: Tratamentos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tratamento = await _context.Tratamento.FindAsync(id);
            TratamentoSelecionado = tratamento.TipodeTratamento;
            if (tratamento == null)
            {
                return NotFound();
            }
            return View(tratamento);
        }
        Tratamento verificarTratamento2,verificarTratamento;
        // POST: Tratamentos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
            
        public async Task<IActionResult> Edit(int id, [Bind("TratamentoId,TipodeTratamento")] Tratamento tratamento)
        {
            verificarTratamento = _context.Tratamento.SingleOrDefault(p => p.TipodeTratamento == tratamento.TipodeTratamento);
           // verificarTratamento2 = await _context.Tratamento.SingleOrDefaultAsync(p => p.TratamentoId == tratamento.TratamentoId);
          /*  if (id != tratamento.TratamentoId)
            {
                return NotFound();
            }*/
            

            if (ModelState.IsValid)
            {
                // if (verificarTratamento2.TipodeTratamento==tratamento.TipodeTratamento)
                if (tratamento.TipodeTratamento == TratamentoSelecionado) {
                    ViewBag.Message = "editarOP";
                    return View("TratamentoCriado", tratamento);
                } if(verificarTratamento==null) 
                  {
                    try
                    {
                        _context.Update(tratamento);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!TratamentoExists(tratamento.TratamentoId))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    ViewBag.Message = "editarOP";
                     return View("TratamentoCriado", tratamento);
                }
                else {
                    ViewBag.Message = "Tratamento já existente.";
                    return View("Edit");
                }
            }
            return View(tratamento);
        }

        // GET: Tratamentos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tratamento = await _context.Tratamento
                .FirstOrDefaultAsync(m => m.TratamentoId == id);
            if (tratamento == null)
            {
                return NotFound();
            }

            return View(tratamento);
        }

        // POST: Tratamentos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            
            var tratamento = await _context.Tratamento.FindAsync(id);
          //  var tratamento1 = await _context.Tratamento.FindAsync(id);
            
            _context.Tratamento.Remove(tratamento);
             await _context.SaveChangesAsync();
            return View("TratamentoApagado",tratamento);//RedirectToAction(nameof(Index));
        }

        private bool TratamentoExists(int id)
        {
            return _context.Tratamento.Any(e => e.TratamentoId == id);
        }
    }
}

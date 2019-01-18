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
    public class ReceitarTratamentosController : Controller
    {
        private const int PAGE_SIZE = 4;
        private readonly ServicoDomicilioDbContext _context;

        public ReceitarTratamentosController(ServicoDomicilioDbContext context)
        {
            _context = context;
        }

        // GET: ReceitarTratamentos
        public async Task<IActionResult> Index(ReceitaTratamentoListViewModel model = null, int page = 1)
        {
            string nome = null;

            if (model != null)
            {
                nome = model.CurrentName;
                //page = 1;
            }

            var receitaTratamento = _context.ReceitarTratamento.Include(r => r.receita).Include(r => r.tratamento)
                .Where(p => nome == null || p.tratamento.TipodeTratamento.Contains(nome));

            int numReceitas = await receitaTratamento.CountAsync();

            if (page > (numReceitas / PAGE_SIZE) + 1)
            {
                page = 1;
            }

            var ReceitaTratamentoList = await receitaTratamento
                    .OrderBy(p => p.ReceitaId)
                    .Skip(PAGE_SIZE * (page - 1))
                    .Take(PAGE_SIZE)
                    .ToListAsync();

            return View(
                new ReceitaTratamentoListViewModel
                {
                    ReceitaTratamento = ReceitaTratamentoList,
                    Pagination = new PagingViewModel
                    {
                        CurrentPage = page,
                        PageSize = PAGE_SIZE,
                        Totaltems = numReceitas
                    },
                    CurrentName = nome
                }
            );
            //var servicoDomicilioDbContext = _context.ReceitarTratamento.Include(r => r.receita).Include(r => r.tratamento);
            //return View(await servicoDomicilioDbContext.ToListAsync());
        }

        // GET: ReceitarTratamentos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var receitarTratamento = await _context.ReceitarTratamento
                .Include(r => r.receita)
                .Include(r => r.tratamento)
                .FirstOrDefaultAsync(m => m.ReceitarTratamentoId == id);
            if (receitarTratamento == null)
            {
                return NotFound();
            }

            return View(receitarTratamento);
        }

        // GET: ReceitarTratamentos/Create
        public IActionResult Create(Receita receita)
        {
            IList<Tratamento> TratamentoList = new List<Tratamento>();

            // ViewData["ReceitaId"] = new SelectList(_context.Receita, "ReceitaId", "ReceitaId");
            foreach (var item in _context.Tratamento)
            {
                TratamentoList.Add(new Tratamento() { TratamentoId = item.TratamentoId, TipodeTratamento = item.TipodeTratamento });
            }
            ViewData["Tratamentos"] = TratamentoList;
            // ViewData["TratamentoId"] = new SelectList(_context.Tratamento, "TratamentoId", "TipodeTratamento");
            return View();
        }

        // POST: ReceitarTratamentos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReceitarTratamentoId,ReceitaId,TratamentoId")] ReceitarTratamento receitarTratamento)
        {
            string selected = Request.Form["SelectedTratamento"].ToString();
            string[] selectedList = selected.Split(',');

            if (ModelState.IsValid)
            {
                // receitarTratamento.ReceitaId = receita.ReceitaId;

                int IDultimareceita;
                foreach (var item in selectedList)
                {
                    IDultimareceita = _context.Receita.Max(p => p.ReceitaId);
                    int idTratamento = Convert.ToInt32(item);
                    // Tratamento tratamento = _contex
                    receitarTratamento.TratamentoId = idTratamento;
                    receitarTratamento.ReceitaId = IDultimareceita;
                    _context.ReceitarTratamento.Add(new ReceitarTratamento { ReceitaId = receitarTratamento.ReceitaId, TratamentoId = receitarTratamento.TratamentoId });
                    _context.SaveChanges();
                    // _context.Add(receitarTratamento);
                    //await _context.SaveChangesAsync();


                    //return RedirectToAction("Create", "ReceitarTratamentos");
                }
                // await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
            }
            // ViewData["ReceitaId"] = new SelectList(_context.Receita, "ReceitaId", "ReceitaId", receitarTratamento.ReceitaId);
            // ViewData["TratamentoId"] = new SelectList(_context.Tratamento, "TratamentoId", "TipodeTratamento", receitarTratamento.TratamentoId);
            // return View(receitarTratamento);
            return RedirectToAction("Index", "Receitas");
            
        }

        // GET: ReceitarTratamentos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var receitarTratamento = await _context.ReceitarTratamento.FindAsync(id);
            if (receitarTratamento == null)
            {
                return NotFound();
            }
            ViewData["ReceitaId"] = new SelectList(_context.Receita, "ReceitaId", "ReceitaId", receitarTratamento.ReceitaId);
            ViewData["TratamentoId"] = new SelectList(_context.Tratamento, "TratamentoId", "TipodeTratamento", receitarTratamento.TratamentoId);
            return View(receitarTratamento);
        }

        // POST: ReceitarTratamentos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReceitarTratamentoId,ReceitaId,TratamentoId")] ReceitarTratamento receitarTratamento)
        {
            if (id != receitarTratamento.ReceitarTratamentoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(receitarTratamento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReceitarTratamentoExists(receitarTratamento.ReceitarTratamentoId))
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
            ViewData["ReceitaId"] = new SelectList(_context.Receita, "ReceitaId", "ReceitaId", receitarTratamento.ReceitaId);
            ViewData["TratamentoId"] = new SelectList(_context.Tratamento, "TratamentoId", "TipodeTratamento", receitarTratamento.TratamentoId);
            return View(receitarTratamento);
        }

        // GET: ReceitarTratamentos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var receitarTratamento = await _context.ReceitarTratamento
                .Include(r => r.receita)
                .Include(r => r.tratamento)
                .FirstOrDefaultAsync(m => m.ReceitarTratamentoId == id);
            if (receitarTratamento == null)
            {
                return NotFound();
            }

            return View(receitarTratamento);
        }

        // POST: ReceitarTratamentos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var receitarTratamento = await _context.ReceitarTratamento.FindAsync(id);
            _context.ReceitarTratamento.Remove(receitarTratamento);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReceitarTratamentoExists(int id)
        {
            return _context.ReceitarTratamento.Any(e => e.ReceitarTratamentoId == id);
        }
    }
}

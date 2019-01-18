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
    public class HorarioTrabalhoController : Controller
    {
        private const int PAGE_SIZE = 3;
        private readonly ServicoDomicilioDbContext _context;

        public HorarioTrabalhoController(ServicoDomicilioDbContext context)
        {
            _context = context;
        }

        // GET: HorarioTrabalho
        public async Task<IActionResult> Index(HorarioTrabalhoListViewModel model = null, int page = 1, string order = null)
        {


            string name = null;

            if (model != null)
            {
                name = model.CurrentName;
            }

            var horarios = _context.HorarioTrabalho
                .Where(p => name == null || p.Enfermeiros.Nome.Contains(name))
                .Include(p => p.Enfermeiros);
                

            int numHorarios = await horarios.CountAsync();

            IEnumerable<HorarioTrabalho> horariosList;

            if (order == "name")
            {
                horariosList = await horarios
                    .OrderBy(p => p.Enfermeiros.Nome)
                    .Skip(PAGE_SIZE * (page - 1))
                    .Take(PAGE_SIZE)
                    .ToListAsync();
            }
            else if (order == "data")
            {
                horariosList = await horarios
                   .OrderBy(p => p.Data)
                   .Skip(PAGE_SIZE * (page - 1))
                   .Take(PAGE_SIZE)
                   .ToListAsync();
            }
            else if(order == "horaInicio")
            {
                horariosList = await horarios
                    .OrderBy(p => p.HoraInicio)
                    .Skip(PAGE_SIZE * (page - 1))
                    .Take(PAGE_SIZE)
                    .ToListAsync();
            }
            else
            {
                horariosList = await horarios
                    .OrderBy(p => p.HoraFim)
                    .Skip(PAGE_SIZE * (page - 1))
                    .Take(PAGE_SIZE)
                    .ToListAsync();
            }

            return View(
                new HorarioTrabalhoListViewModel
                {
                    Horarios = horariosList,
                    Pagination = new PagingViewModel
                    {
                        CurrentPage = page,
                        PageSize = PAGE_SIZE,
                        Totaltems = numHorarios,
                        Order = order
                    },
                    CurrentName = name,
                }
            );


            //var servicoDomicilioDbContext = _context.HorarioTrabalho.Include(h => h.Enfermeiros);
            //return View(await servicoDomicilioDbContext.ToListAsync());
        }

        ///////////////////////////////////////////////////////
        public async Task  <IActionResult> VisualizarTroca()
        {
            //HorarioTrabalho  aux;
            HorarioTrabalho horario;
            HorarioTrabalho horario1;
            //HorarioTrabalho trocou;
            Enfermeiros nome;
            Enfermeiros nomee;
            IList<Troca> TrocaList = new List<Troca>();
            foreach (var item in _context.Troca )
            {
                    if (item.Aprovar == true)
                    {
                        //Troca = true;

                        //trocou = _context.HorarioTrabalho.SingleOrDefault(p => p.Troca = item.Aprovar);

                        horario = _context.HorarioTrabalho.SingleOrDefault(p => p.HorarioTrabalhoId == item.HorarioTrabalhoAntigoId);
                        horario1 = _context.HorarioTrabalho.SingleOrDefault(p => p.HorarioTrabalhoId == item.HorarioTrabalhoId);

                        nome = _context.Enfermeiros.SingleOrDefault(p => p.EnfermeirosId == item.EnfermeirosId);
                        nomee = _context.Enfermeiros.SingleOrDefault(p => p.EnfermeirosId == item.EnfermeirosEId);

                        TrocaList.Add(new Troca() { HorarioTrabalhoId = item.HorarioTrabalhoId, TrocaId = item.TrocaId, EnfermeiroRequerente = nome,
                        EnfermeiroEscolhido = nomee, HorarioTrabalhoAntigo = horario1, HorarioTrabalhoNovo = horario }); 
                        ViewBag.Message = "" + item.HorarioTrabalhoId.ToString();

                        //_context.Add(horario);
                         //_context.Add(horario1);
                     await _context.SaveChangesAsync();
                        //return RedirectToAction(nameof(VisualizarTroca));


                    }
                    else
                    {
                     ViewBag.Message= "Não existe trocas efetuadas";
                }
            }
            ViewData["VisualizarTroca"] = TrocaList;
            return View(TrocaList); 
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
        [Authorize(Policy = "OnlyAdminAccess")]
        public IActionResult Create()
        {
            ViewData["EnfermeirosId"] = new SelectList(_context.Enfermeiros, "EnfermeirosId", "Nome");
            return View();
        }

        // POST: HorarioTrabalho/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "OnlyAdminAccess")]
        public async Task<IActionResult> Create([Bind("HorarioTrabalhoId,Data,HoraInicio,HoraFim,EnfermeirosId,Troca")] HorarioTrabalho horarioTrabalho)
        {
            if (ModelState.IsValid)
            {
                // verificar Enfermeiro
                HorarioTrabalho verificarHorario = _context.HorarioTrabalho.SingleOrDefault(p => p.EnfermeirosId == horarioTrabalho.EnfermeirosId);
                HorarioTrabalho verificarHorario1 = _context.HorarioTrabalho.SingleOrDefault(p => p.HoraInicio == horarioTrabalho.HoraInicio);

                if (verificarHorario == null && verificarHorario1 == null) {
                    _context.Add(horarioTrabalho);
                    await _context.SaveChangesAsync();
                    return View("HorarioTrabalho", horarioTrabalho);
                    //return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Message = "Horario já existe!";
                    return View("Create");
                }
             //ViewData["EnfermeirosId"] = new SelectList(_context.Enfermeiros, "EnfermeirosId", "Nome", horarioTrabalho.EnfermeirosId);
            }
            return View(horarioTrabalho);
        }

        // GET: HorarioTrabalho/Edit/5
        [Authorize(Policy = "OnlyAdminAccess")]
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
            ViewData["EnfermeirosId"] = new SelectList(_context.Enfermeiros, "EnfermeirosId", "Nome", horarioTrabalho.EnfermeirosId);
            return View(horarioTrabalho);
        }

        // POST: HorarioTrabalho/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "OnlyAdminAccess")]
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
            ViewData["EnfermeirosId"] = new SelectList(_context.Enfermeiros, "EnfermeirosId", "Nome", horarioTrabalho.EnfermeirosId);
            return View(horarioTrabalho);
        }

        // GET: HorarioTrabalho/Delete/5
        [Authorize(Policy = "OnlyAdminAccess")]
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
        [Authorize(Policy = "OnlyAdminAccess")]
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

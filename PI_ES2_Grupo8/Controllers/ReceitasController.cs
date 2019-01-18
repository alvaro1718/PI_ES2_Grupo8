using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PI_ES2_Grupo8.Models;
namespace PI_ES2_Grupo8.Controllers
{
    public class ReceitasController : Controller
    {
        private const int PAGE_SIZE = 4;
        private readonly ServicoDomicilioDbContext _context;

        //  private readonly Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityDbContext _2context;
        
        public ReceitasController(ServicoDomicilioDbContext context)
        {
            _context = context;
           // Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityDbContext identityDbContext
          //  _2context = identityDbContext;
        }

        // GET: Receitas
        public async Task<IActionResult> Index(ReceitasListViewModel model=null, int page=1)
        {
            string nome = null;

            if (model != null)
            {
                nome = model.CurrentName;
                //page = 1;
            }

            var receita = _context.Receita.Include(r => r.medico).Include(r => r.utente)
                .Where(p => nome == null || p.medico.Nome.Contains(nome));

            int numReceitas = await receita.CountAsync();

            if (page > (numReceitas / PAGE_SIZE) + 1)
            {
                page = 1;
            }

            var ReceitasList = await receita
                    .OrderBy(p => p.ReceitaId)
                    .Skip(PAGE_SIZE * (page - 1))
                    .Take(PAGE_SIZE)
                    .ToListAsync();

            return View(
                new ReceitasListViewModel
                {
                    Receitas = ReceitasList,
                    Pagination = new PagingViewModel
                    {
                        CurrentPage = page,
                        PageSize = PAGE_SIZE,
                        Totaltems = numReceitas
                    },
                    CurrentName = nome
                }
            );
            //var servicoDomicilioDbContext = _context.Receita.Include(r => r.medico).Include(r => r.utente);
            // return View(await servicoDomicilioDbContext.ToListAsync());
        }

        // GET: Receitas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            // ReceitarTratamento receitarTratamento;
            Tratamento tratamento;
            IList<ReceitarTratamento> ReceitaTratamentoList = new List<ReceitarTratamento>();
            // selecionar todas os tratamentos relacionados com a receita selecionada
            foreach (var item in _context.ReceitarTratamento)
            {
                if (item.ReceitaId==id) {
                    tratamento = _context.Tratamento.SingleOrDefault(p => p.TratamentoId == item.TratamentoId);
                    ReceitaTratamentoList.Add(new ReceitarTratamento() {ReceitaId=item.ReceitaId, TratamentoId = item.TratamentoId,tratamento=tratamento});
                    ViewBag.Message = ""+item.ReceitaId.ToString();
                }
                //receitarTratamento = _context.ReceitarTratamento.//Where(p => p.ReceitaId == id);

            }
            ViewData["ReceitaTratamentos"] = ReceitaTratamentoList;
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
           
            //string userName;

           // var userId = User.Identity.IsAuthenticated;
            //string userId2 = User.Identity.;
            //  Medico medico = _context.Medico.SingleOrDefault(a => a.Email == );
            
            DateTime date = DateTime.Now;
            if (ModelState.IsValid)
            {
               /* if (SignInManager.IsSignedIn(User))
                {
                    userName = UserManager.GetUserName(User);
                    medicoLogin = _context.Medico.SingleOrDefault(a => a.Email == userName);
                }
                receita.medico = medicoLogin;*/
                receita.Date = date;
                int ultimareceita = _context.Receita.Max(p=>p.Nreceita);
                receita.Nreceita = ultimareceita+1;
                _context.Add(receita);
                await _context.SaveChangesAsync();
                //_context.Receita.Add(new Receita { MedicoId = receita.MedicoId, UtenteId = receita.UtenteId, medico = receita.medico, utente = receita.utente, Nreceita = ultimareceita + 1 });
                //_context.SaveChanges();
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
            
            Tratamento tratamento;
            IList<ReceitarTratamento> ReceitaTratamentoList = new List<ReceitarTratamento>();
            // selecionar todas os tratamentos relacionados com a receita selecionada
            foreach (var item in _context.ReceitarTratamento)
            {
                if (item.ReceitaId == id)
                {
                    tratamento = _context.Tratamento.SingleOrDefault(p => p.TratamentoId == item.TratamentoId);
                    ReceitaTratamentoList.Add(new ReceitarTratamento() { ReceitaId = item.ReceitaId, TratamentoId = item.TratamentoId, tratamento = tratamento });
                    ViewBag.Message = "" + item.ReceitaId.ToString();
                }
                //receitarTratamento = _context.ReceitarTratamento.//Where(p => p.ReceitaId == id);

            }
            ViewData["ReceitaTratamentos"] = ReceitaTratamentoList;
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

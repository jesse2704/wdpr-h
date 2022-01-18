using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using wdpr_h.Data;
using wdpr_h.Models;

namespace wdpr_h.Controllers
{
    public class ClientZelfhulpgroepController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClientZelfhulpgroepController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ClientZelfhulpgroep
        public async Task<IActionResult> Index()
        {
            return View(await _context.ClientZelfhulpgroep.ToListAsync());
        }

        public async Task<IActionResult> IndexClient(string sorteer,string zoek, int pagina)
        {
            if (sorteer == null) sorteer = "naam_oplopend";
            ViewData["sorteer"] = sorteer;
            ViewData["pagina"] = pagina;
            ViewData["heeftVolgende"] = (pagina + 1) * 10  < _context.Zelfhulpgroep.Count();
            ViewData["heeftVorige"] = pagina > 0;
            ViewData["clientZelfhulpgroepList"] = _context.ClientZelfhulpgroep.ToList();

                var zelfhulpgroepList = _context.Zelfhulpgroep;
                return View(Pagineer(
                                    Zoek(
                                        Sorteer(zelfhulpgroepList, sorteer)
                                        , zoek)
                                    , pagina, 10)
                            .ToList());

            //ClientZelfhulpgroepViewModel clientZelfhulpgroepViewModel = new ClientZelfhulpgroepViewModel();


            //clientZelfhulpgroepViewModel.ZelfhulpgroepList = _context.Zelfhulpgroep.ToList();
            //clientZelfhulpgroepViewModel.ClientZelfhulpgroep = _context.ClientZelfhulpgroep.ToList();

            //return View(await Pagineer(Zoek(Sorteer(clientZelfhulpgroepViewModel, sorteer),  zoek), pagina, 10).ToListAsync());
            //return View(clientZelfhulpgroepViewModel);
        }
        public IQueryable<Zelfhulpgroep> Sorteer(IQueryable<Zelfhulpgroep> lijst, string sorteer)
        {

            if (sorteer == "naam_oplopend") return lijst.OrderBy(z => z.Titel.ToLower());
                else 
                return lijst.OrderByDescending(h => h.Titel);
        
        }
        public  IQueryable<Zelfhulpgroep> Zoek(IQueryable<Zelfhulpgroep> lijst, string zoek)
        {
            if(zoek == null) return lijst;
                else

            return lijst.Where(z => z.Titel.ToLower().Contains(zoek.ToLower()));
        }
        public  IQueryable<Zelfhulpgroep> Pagineer(IQueryable<Zelfhulpgroep> lijst, int pagina, int aantal)
        {
            if (pagina < 0) pagina = 0;

            return lijst.Skip(pagina * aantal).Take(aantal);
        }

        // GET: ClientZelfhulpgroep/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientZelfhulpgroep = await _context.ClientZelfhulpgroep
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clientZelfhulpgroep == null)
            {
                return NotFound();
            }

            return View(clientZelfhulpgroep);
        }

        // GET: ClientZelfhulpgroep/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ClientZelfhulpgroep/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdClient,IdGroep")] ClientZelfhulpgroep clientZelfhulpgroep)
        {
            if (ModelState.IsValid)
            {
                _context.Add(clientZelfhulpgroep);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(clientZelfhulpgroep);
        }

        // GET: ClientZelfhulpgroep/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientZelfhulpgroep = await _context.ClientZelfhulpgroep.FindAsync(id);
            if (clientZelfhulpgroep == null)
            {
                return NotFound();
            }
            return View(clientZelfhulpgroep);
        }

        // POST: ClientZelfhulpgroep/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdClient,IdGroep")] ClientZelfhulpgroep clientZelfhulpgroep)
        {
            if (id != clientZelfhulpgroep.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clientZelfhulpgroep);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientZelfhulpgroepExists(clientZelfhulpgroep.Id))
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
            return View(clientZelfhulpgroep);
        }

        // GET: ClientZelfhulpgroep/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientZelfhulpgroep = await _context.ClientZelfhulpgroep
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clientZelfhulpgroep == null)
            {
                return NotFound();
            }

            return View(clientZelfhulpgroep);
        }

        // POST: ClientZelfhulpgroep/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var clientZelfhulpgroep = await _context.ClientZelfhulpgroep.FindAsync(id);
            _context.ClientZelfhulpgroep.Remove(clientZelfhulpgroep);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientZelfhulpgroepExists(int id)
        {
            return _context.ClientZelfhulpgroep.Any(e => e.Id == id);
        }


        [HttpGet]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> Aanmelden(Guid id)
        {
            //Haal de user GUID op
            var userId =  User.FindFirstValue(ClaimTypes.NameIdentifier);

            //Voeg nieuwe client toe met bijbehorende gegevens
            ClientZelfhulpgroep newClientZelfhulpgroep = new ClientZelfhulpgroep();

            //Parse de opgehaalde String om naar Guid
            newClientZelfhulpgroep.IdClient = Guid.Parse(userId);
            
            newClientZelfhulpgroep.IdGroep = id;
            _context.ClientZelfhulpgroep.Add(newClientZelfhulpgroep);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}

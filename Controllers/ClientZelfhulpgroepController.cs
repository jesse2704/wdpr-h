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

        public IActionResult IndexClient()
        {
            ClientZelfhulpgroepViewModel clientZelfhulpgroepViewModel = new ClientZelfhulpgroepViewModel();


            clientZelfhulpgroepViewModel.ZelfhulpgroepList = _context.Zelfhulpgroep.ToList();
            clientZelfhulpgroepViewModel.ClientZelfhulpgroep = _context.ClientZelfhulpgroep.ToList();

            return View(clientZelfhulpgroepViewModel);
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

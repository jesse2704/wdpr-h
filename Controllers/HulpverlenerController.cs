using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using wdpr_h.Data;
using wdpr_h.Models;

namespace wdpr_h.Controllers
{
    public class HulpverlenerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HulpverlenerController(ApplicationDbContext context)
        {
            _context = context;
        }

         public IActionResult Orthopedagoog()
        {
            return View();
        }

        //sorteermethode
        public  IQueryable<Hulpverlener> Sorteer(IQueryable<Hulpverlener> lijst, string sorteer)
        {

            if (sorteer == "naam_oplopend")

                return lijst.OrderBy(h => h.Naam.ToLower());
            else 
                return lijst.OrderByDescending(h => h.Naam);
        
        }

         //zoekmethode
        public  IQueryable<Hulpverlener> Zoek(IQueryable<Hulpverlener> lijst, string zoek)
        {
            if(zoek == null)
            return lijst;
            else
            return lijst.Where(h => h.Naam.ToLower().Contains(zoek.ToLower()));
        }
        //Pagineren
        public  IQueryable<Hulpverlener> Pagineer(IQueryable<Hulpverlener> lijst, int pagina, int aantal)
        {
            if (pagina < 0) 
                pagina = 0;
            return lijst.Skip(pagina * aantal).Take(aantal);
        }

        // GET: Hulpverlener

        public async Task<IActionResult> Index(string sorteer,string zoek, int pagina)
        {
            if (sorteer == null) sorteer = "naam_oplopend";
            ViewData["sorteer"] = sorteer;
            ViewData["pagina"] = pagina;
            ViewData["heeftVolgende"] = (pagina + 1) * 10  < _context.Hulpverlener.Count();
            ViewData["heeftVorige"] = pagina > 0;
            return View(await Pagineer(Zoek(Sorteer(_context.Hulpverlener, sorteer),  zoek), pagina, 10).ToListAsync());
        }
        
        
        // {
        //     if (sorteer == null) sorteer = "naam_oplopend";
        //     ViewData["sorteer"] = sorteer;
        //     return View(await Sorteer(_context.Hulpverlener, sorteer).ToListAsync());
        // }


        // public async Task<IActionResult> Index()
        // {
        //     return View(await _context.Hulpverlener.ToListAsync());
        // }

        // GET: Hulpverlener/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hulpverlener = await _context.Hulpverlener
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hulpverlener == null)
            {
                return NotFound();
            }

            return View(hulpverlener);
        }

        // GET: Hulpverlener/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Hulpverlener/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Naam,Achternaam,Specialisme,Wachtwoord,Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount")] Hulpverlener hulpverlener)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hulpverlener);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(hulpverlener);
        }

        // GET: Hulpverlener/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hulpverlener = await _context.Hulpverlener.FindAsync(id);
            if (hulpverlener == null)
            {
                return NotFound();
            }
            return View(hulpverlener);
        }

        // POST: Hulpverlener/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Naam,Achternaam,Specialisme,Wachtwoord,Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount")] Hulpverlener hulpverlener)
        {
            if (id != hulpverlener.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hulpverlener);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HulpverlenerExists(hulpverlener.Id))
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
            return View(hulpverlener);
        }

        // GET: Hulpverlener/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hulpverlener = await _context.Hulpverlener
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hulpverlener == null)
            {
                return NotFound();
            }

            return View(hulpverlener);
        }

        // POST: Hulpverlener/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var hulpverlener = await _context.Hulpverlener.FindAsync(id);
            _context.Hulpverlener.Remove(hulpverlener);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HulpverlenerExists(string id)
        {
            return _context.Hulpverlener.Any(e => e.Id == id);
        }
    }
}

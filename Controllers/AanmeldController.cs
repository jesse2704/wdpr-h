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
    public class AanmeldController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AanmeldController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Aanmeld
        public async Task<IActionResult> Index()
        {
            
            return View(await _context.Aanmeld.ToListAsync());
        }

        // GET: Aanmeld/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aanmeld = await _context.Aanmeld
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aanmeld == null)
            {
                return NotFound();
            }

            return View(aanmeld);
        }

        // GET: Aanmeld/Create
        public IActionResult Create()
        {
            AanmeldViewModel aanmeldViewModel = new AanmeldViewModel();
            aanmeldViewModel.Hulpverleners = _context.Hulpverlener.ToList();
            
            return View(aanmeldViewModel);
        }

        // POST: Aanmeld/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Naam,Achternaam,Geboortedatum,Aandoening,Email,hulpVerlenerId,voorKind,heeftVerwijzing")] Aanmeld aanmeld)
        {
            if (ModelState.IsValid)
            {
                aanmeld.Id = Guid.NewGuid();
                _context.Add(aanmeld);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(aanmeld);
        }

        // GET: Aanmeld/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aanmeld = await _context.Aanmeld.FindAsync(id);
            if (aanmeld == null)
            {
                return NotFound();
            }
            return View(aanmeld);
        }

        // POST: Aanmeld/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Naam,Achternaam,Geboortedatum,Aandoening,Email,hulpVerlenerId,voorKind,heeftVerwijzing")] Aanmeld aanmeld)
        {
            if (id != aanmeld.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aanmeld);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AanmeldExists(aanmeld.Id))
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
            return View(aanmeld);
        }

        // GET: Aanmeld/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aanmeld = await _context.Aanmeld
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aanmeld == null)
            {
                return NotFound();
            }

            return View(aanmeld);
        }

        // POST: Aanmeld/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var aanmeld = await _context.Aanmeld.FindAsync(id);
            _context.Aanmeld.Remove(aanmeld);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AanmeldExists(Guid id)
        {
            return _context.Aanmeld.Any(e => e.Id == id);
        }
    }
}

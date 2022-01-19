using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using wdpr_h.Data;
using wdpr_h.Models;
using System.Security.Claims;

namespace wdpr_h.Controllers
{
    public class ZelfhulpgroepController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ZelfhulpgroepController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Zelfhulpgroep
        public async Task<IActionResult> Index()
        {
            return View(await _context.Zelfhulpgroep.ToListAsync());
        }

        // GET: Zelfhulpgroep/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zelfhulpgroep = await _context.Zelfhulpgroep
                .FirstOrDefaultAsync(m => m.Id == id);
            if (zelfhulpgroep == null)
            {
                return NotFound();
            }

            return View(zelfhulpgroep);
        }

        // GET: Zelfhulpgroep/Create
        //[Authorize(Roles = "Hulpverlener")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Zelfhulpgroep/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titel,Onderwerp,LeeftijdsCategorie")] Zelfhulpgroep zelfhulpgroep)
        {
            if (ModelState.IsValid)
            {
                zelfhulpgroep.Id = Guid.NewGuid();
                _context.Add(zelfhulpgroep);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(zelfhulpgroep);
        }

        // GET: Zelfhulpgroep/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zelfhulpgroep = await _context.Zelfhulpgroep.FindAsync(id);
            if (zelfhulpgroep == null)
            {
                return NotFound();
            }
            return View(zelfhulpgroep);
        }

        // POST: Zelfhulpgroep/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Titel,Onderwerp,LeeftijdsCategorie")] Zelfhulpgroep zelfhulpgroep)
        {
            if (id != zelfhulpgroep.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(zelfhulpgroep);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ZelfhulpgroepExists(zelfhulpgroep.Id))
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
            return View(zelfhulpgroep);
        }

        // GET: Zelfhulpgroep/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zelfhulpgroep = await _context.Zelfhulpgroep
                .FirstOrDefaultAsync(m => m.Id == id);
            if (zelfhulpgroep == null)
            {
                return NotFound();
            }

            return View(zelfhulpgroep);
        }

        // POST: Zelfhulpgroep/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var zelfhulpgroep = await _context.Zelfhulpgroep.FindAsync(id);
            _context.Zelfhulpgroep.Remove(zelfhulpgroep);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ZelfhulpgroepExists(Guid id)
        {
            return _context.Zelfhulpgroep.Any(e => e.Id == id);
        }

    }
}

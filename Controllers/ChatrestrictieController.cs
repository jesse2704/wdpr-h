using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using wdpr_h.Models;

namespace wdpr_h.Controllers
{
    public class ChatrestrictieController : Controller
    {
        private readonly MyContext _context;

        public ChatrestrictieController(MyContext context)
        {
            _context = context;
        }

        // GET: Chatrestrictie
        public async Task<IActionResult> Index()
        {
            return View(await _context.Chatrestrictie.ToListAsync());
        }

        // GET: Chatrestrictie/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chatrestrictie = await _context.Chatrestrictie
                .FirstOrDefaultAsync(m => m.Id == id);
            if (chatrestrictie == null)
            {
                return NotFound();
            }

            return View(chatrestrictie);
        }

        // GET: Chatrestrictie/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Chatrestrictie/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,User,BeginTijd,EindTijd,Reden,ZelfhulpgroepId")] Chatrestrictie chatrestrictie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(chatrestrictie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(chatrestrictie);
        }

        // GET: Chatrestrictie/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chatrestrictie = await _context.Chatrestrictie.FindAsync(id);
            if (chatrestrictie == null)
            {
                return NotFound();
            }
            return View(chatrestrictie);
        }

        // POST: Chatrestrictie/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,User,BeginTijd,EindTijd,Reden,ZelfhulpgroepId")] Chatrestrictie chatrestrictie)
        {
            if (id != chatrestrictie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chatrestrictie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChatrestrictieExists(chatrestrictie.Id))
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
            return View(chatrestrictie);
        }

        // GET: Chatrestrictie/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chatrestrictie = await _context.Chatrestrictie
                .FirstOrDefaultAsync(m => m.Id == id);
            if (chatrestrictie == null)
            {
                return NotFound();
            }

            return View(chatrestrictie);
        }

        // POST: Chatrestrictie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var chatrestrictie = await _context.Chatrestrictie.FindAsync(id);
            _context.Chatrestrictie.Remove(chatrestrictie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChatrestrictieExists(int id)
        {
            return _context.Chatrestrictie.Any(e => e.Id == id);
        }
    }
}

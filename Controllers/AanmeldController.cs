using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NToastNotify;
using wdpr_h.Data;
using wdpr_h.Models;
using wdpr_h.Services;

namespace wdpr_h.Controllers
{
    public class AanmeldController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IToastNotification _toastNotification;

        private IMailService _mailService;

        public AanmeldController(ApplicationDbContext context, UserManager<IdentityUser> userManager, IToastNotification toastNotification, IMailService mailservice)
        {
            _context = context;
            _userManager = userManager;
            _toastNotification = toastNotification;
            _mailService = mailservice;
        }

        // GET: Aanmeld
        [Authorize(Roles = "Hulpverlener")]
        public async Task<IActionResult> Index()
        {
              //Haal de user GUID op
            var hulpverlenerId =  User.FindFirstValue(ClaimTypes.NameIdentifier);

            return View(await _context.Aanmeld.Where(a => a.hulpVerlenerId == Guid.Parse(hulpverlenerId)).ToListAsync());
        }

        // GET: Aanmeld/Details/5
        [Authorize(Roles = "Hulpverlener")]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aanmeld = await _context.Aanmeld
                .FirstOrDefaultAsync(m => m.Id == id);

            var account = ViewData["checkIfAccountExist"] = _context.Client.Any(c => c.Email == aanmeld.Email);
            if ((bool)account)
            {
                //var user = ViewData["GetAccount"] = _context.Client.Where(c => c.Email == aanmeld.Email).Single();
                
                ViewData["getIdClient"] = _context.Client.Where(c => c.Email == aanmeld.Email).SingleOrDefault().Id;
                //ViewData["hasParent"] = _context.Client.Where(c => c.OuderAccount != Guid.Parse("00000000-0000-0000-0000-000000000000")).Any();
            }
            Boolean checkParent =  _context.Client.Where(c => c.Email == aanmeld.Email).Any(c => c.OuderAccount != Guid.Parse("00000000-0000-0000-0000-000000000000"));
            ViewData["hasParent"] = checkParent;

            
            
            
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
                _toastNotification.AddSuccessToastMessage("Succesvol aangemeld");
                return RedirectToAction(nameof(Index));
            }
            return View(aanmeld);
        }

        // GET: Aanmeld/Edit/5
        [Authorize(Roles = "Hulpverlener")]
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
                _toastNotification.AddSuccessToastMessage("Client is aangepast");
                return RedirectToAction(nameof(Index));
            }
            return View(aanmeld);
        }

        // GET: Aanmeld/Delete/5
        [Authorize(Roles = "Hulpverlener")]
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
            _toastNotification.AddSuccessToastMessage("Client is verwijderd");
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

        [HttpGet]
        [Authorize(Roles = "Hulpverlener")]
        public async Task<IActionResult> CreateNewAccount(Guid id)
        {
            if (ModelState.IsValid)
            {
                var targetAanmeld = _context.Aanmeld.Single(a => a.Id == id);
                var tijdelijkWachtwoord = GeneratePassword();
                
                
                var user = new Client 
                { UserName = targetAanmeld.Email, Email = targetAanmeld.Email,
                Nicknaam = "New user", LeeftijdsCategorie = "Nog wijzigen",
                Naam = targetAanmeld.Naam, Achternaam = targetAanmeld.Achternaam,
                isKindAccount = targetAanmeld.voorKind, Geboortedatum = targetAanmeld.Geboortedatum, HulpverlenerId = targetAanmeld.hulpVerlenerId};

                var result = await _userManager.CreateAsync(user, tijdelijkWachtwoord);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Client");
                    //_logger.LogInformation("User created a new account with password.");

                }
                
                //Send email with temp password
                Hulpverlener hulpverlener = _context.Hulpverlener.Single(h => h.Id == targetAanmeld.hulpVerlenerId.ToString());
                await _mailService.SendEmailAsync(targetAanmeld.Email, "Wachtwoord om in te loggen", "<h1>U kunt nu inloggen!! </h1><br>" + tijdelijkWachtwoord);
                _toastNotification.AddSuccessToastMessage("Client " + user.Naam + " is aangemaakt!");
                var test = _context.Aanmeld.Single(u => u.Email == user.Email).Id.ToString();
                return RedirectToAction(nameof(Details), new { id = _context.Aanmeld.Single(u => u.Email == user.Email).Id.ToString() });
            }
            return View();
        }

        [HttpGet]
        public string GeneratePassword()
        {
            var options = _userManager.Options.Password;

            int length = options.RequiredLength;

            bool nonAlphanumeric = options.RequireNonAlphanumeric;
            bool digit = options.RequireDigit;
            bool lowercase = options.RequireLowercase;
            bool uppercase = options.RequireUppercase;

            StringBuilder password = new StringBuilder();
            Random random = new Random();

            while (password.Length < length)
            {
                char c = (char)random.Next(32, 126);

                password.Append(c);

                if (char.IsDigit(c))
                    digit = false;
                else if (char.IsLower(c))
                    lowercase = false;
                else if (char.IsUpper(c))
                    uppercase = false;
                else if (!char.IsLetterOrDigit(c))
                    nonAlphanumeric = false;
            }

            if (nonAlphanumeric)
                password.Append((char)random.Next(33, 48));
            if (digit)
                password.Append((char)random.Next(48, 58));
            if (lowercase)
                password.Append((char)random.Next(97, 123));
            if (uppercase)
                password.Append((char)random.Next(65, 91));

            return password.ToString();
        }
    }
}

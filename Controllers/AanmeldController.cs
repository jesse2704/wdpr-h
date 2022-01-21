using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using FluentEmail.Core;
using FluentEmail.Razor;
using FluentEmail.Smtp;
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

namespace wdpr_h.Controllers
{
    public class AanmeldController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IToastNotification _toastNotification;

        public AanmeldController(ApplicationDbContext context, UserManager<IdentityUser> userManager, IToastNotification toastNotification)
        {
            _context = context;
            _userManager = userManager;
            _toastNotification = toastNotification;
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
                _toastNotification.AddSuccessToastMessage("Client is aangepast");
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
                isKindAccount = targetAanmeld.voorKind, Geboortedatum = targetAanmeld.Geboortedatum};

                var result = await _userManager.CreateAsync(user, tijdelijkWachtwoord);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Client");
                    //_logger.LogInformation("User created a new account with password.");

                }
                
                //Send email with temp password
                Hulpverlener hulpverlener = _context.Hulpverlener.Single(h => h.Id == targetAanmeld.hulpVerlenerId.ToString());
                //SendMail(hulpverlener.Naam, targetAanmeld.Email, targetAanmeld.Naam, tijdelijkWachtwoord, hulpverlener.Email);
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

        public async void SendMail(String senderName, String recieverMail, String recieverName, String randomWachtwoord, String senderMail)
        {
            var sender = new SmtpSender(() => new SmtpClient("localhost")
            {
                EnableSsl = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Port = 25,
                //DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory,
                PickupDirectoryLocation = @"C:\Demos"
            });

            StringBuilder template = new();
            template.AppendLine("Dear @Model.FirstName,");
            template.AppendLine("Er is een account aangemaakt voor u");
            template.AppendLine("Gebruikersnaam = @Model.Email");
            template.AppendLine("Tijdelijke wachtwoord = @Model.Wachtwoord");
            template.AppendLine("- Team ZMDH");

            Email.DefaultSender = sender;
            Email.DefaultRenderer = new RazorRenderer();

            var email = await Email
                .From("@Model.senderEmail")
                .To("@Model.Email", "Sue")
                .Subject("Uw account is aangemaakt!")
                .UsingTemplate(template.ToString(), new { FirstName = recieverName, Email = recieverMail, randomWachtwoord = "null", senderEmail = senderMail })
                //.Body("Thanks for buying our product.")
                .SendAsync();
                
        }
    }
}

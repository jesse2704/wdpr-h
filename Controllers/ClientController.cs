using System;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using FluentEmail.Core;
using FluentEmail.Razor;
using FluentEmail.Smtp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using wdpr_h.Data;
using wdpr_h.Models;

namespace wdpr_h.Controllers
{
    public class ClientController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IToastNotification _toastNotification;

        public ClientController(ApplicationDbContext context, UserManager<IdentityUser> userManager, IToastNotification toastNotification)
        {
            _context = context;
            _userManager = userManager;
            _toastNotification = toastNotification;
        }

        // GET: Client
        public async Task<IActionResult> Index()
        {
            return View(await _context.Client.ToListAsync());
        }

        // GET: Client/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Client
                .FirstOrDefaultAsync(m => m.Id == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // GET: Client/Create
        public IActionResult Create()
        {
            return View();
        }

        //   [HttpGet]
        // public async Task<IActionResult> Create([Bind("Email,Nicknaam,LeeftijdsCategorie,Naam,Achternaam,Geboortedatum")] Client client, Guid id)
        // {
        // if (ModelState.IsValid)
        //     {
        //         client.OuderAccount = id;
        //         var tijdelijkWachtwoord = GeneratePassword();

        //         var result = await _userManager.CreateAsync(client, tijdelijkWachtwoord);
        //         // _context.Add(client);
        //         // await _context.SaveChangesAsync();
        //         return RedirectToAction(nameof(Index));
        //     }
        //     return View(client);
        // }

        // POST: Client/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nicknaam,LeeftijdsCategorie,Naam,Achternaam,isKindAccount,OuderAccount,KindAccount,HulpverlenerId,Wachtwoord,Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount")] Client client)
        {
            if (ModelState.IsValid)
            {
                
                client.UserName = client.Email;
                var tijdelijkWachtwoord = GeneratePassword();
                var result = await _userManager.CreateAsync(client, tijdelijkWachtwoord);
                if (result.Succeeded)
                {
                    //Add role client to account
                    await _userManager.AddToRoleAsync(client, "Parent");

                    //Get Client account and Add parent account 
                    var kindAccount = client.KindAccount.ToString();

                    var targetClient = await _context.Client.Where(c => c.Id ==  kindAccount).SingleOrDefaultAsync();
                    targetClient.OuderAccount = Guid.Parse(kindAccount);
                    await _context.SaveChangesAsync();
                    var hId = targetClient.HulpverlenerId.ToString();
                    
                    Hulpverlener hulpverlener = _context.Hulpverlener.Single(h => h.Id == hId);
                    SendMail(hulpverlener.Naam, targetClient.Email, targetClient.Naam, tijdelijkWachtwoord, hulpverlener.Email);
                }  

                _toastNotification.AddSuccessToastMessage("Client " + client.Naam + " is aangemaakt!");
                return RedirectToAction("Index", "Aanmeld");
            }
            return View(client);
        }

        // GET: Client/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Client.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            return View(client);
        }

        // POST: Client/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Nicknaam,LeeftijdsCategorie,Naam,Achternaam,isKindAccount,OuderAccount,HulpverlenerId,Wachtwoord,Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount")] Client client)
        {
            if (id != client.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(client);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientExists(client.Id))
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
            return View(client);
        }

        // GET: Client/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Client
                .FirstOrDefaultAsync(m => m.Id == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // POST: Client/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var client = await _context.Client.FindAsync(id);
            _context.Client.Remove(client);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientExists(string id)
        {
            return _context.Client.Any(e => e.Id == id);
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

        [HttpGet]
        [Authorize(Roles = "Hulpverlener")]
        public async Task<IActionResult> CreateParentAccount(String id)
        {
            var getClient = await _context.Client.Where(c => c.Email == id).SingleOrDefaultAsync();
            var getClientId = getClient.Id;
            ViewData["getChildAccountId"] = getClientId;
            return View("Create");
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

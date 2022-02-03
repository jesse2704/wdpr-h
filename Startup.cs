using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using wdpr_h.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using wdpr_h.Models;
using NToastNotify;
using wdpr_h.Services;

namespace wdpr_h
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Environment = env;
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
        {

            if (Environment.IsDevelopment())
            {  
                options.UseSqlite(Configuration.GetConnectionString("DefaultConnection"));
            }
            else
            {
                options.UseSqlServer(Configuration.GetConnectionString("ProductionConnection"));
            }
        });

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddRazorPages()
            .AddRazorRuntimeCompilation();

            services.AddMvc().AddNToastNotifyToastr(new ToastrOptions()
            {
                ProgressBar = false,
                PositionClass = ToastPositions.BottomFullWidth
            });

            //Added Mail service
            services.AddTransient<IMailService, SendGridMailService>();

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddIdentityCore<Client>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddIdentityCore<Hulpverlener>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddIdentityCore<Moderator>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddControllersWithViews();
            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdministratorRole",
                    policy => policy.RequireRole("Administrator"));

                options.AddPolicy("RequireModeratorRole",
                    policy => policy.RequireRole("Moderator"));

                options.AddPolicy("RequireClientRole",
                    policy => policy.RequireRole("Client"));

            });

            services.Configure<IdentityOptions>(options =>
    {
        // Password settings.
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireNonAlphanumeric = true;
        options.Password.RequireUppercase = true;
        options.Password.RequiredLength = 6;
        options.Password.RequiredUniqueChars = 1;

        // Lockout settings.
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
        options.Lockout.MaxFailedAccessAttempts = 5;
        options.Lockout.AllowedForNewUsers = true;

        // User settings.
        options.User.AllowedUserNameCharacters =
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
        options.User.RequireUniqueEmail = false;
    });

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

                options.LoginPath = "/Identity/Account/Login";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.SlidingExpiration = true;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlite(Configuration.GetConnectionString("app")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseNToastNotify();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            //Maak custom rollen aan including een super user
            CreateRoles(serviceProvider).GetAwaiter().GetResult();
        }

        private async Task CreateRoles(IServiceProvider serviceProvider)
        {
            //Initialiseer custom rollen
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            string[] roleNames = { "Admin", "Moderator", "Client", "Hulpverlener", "Parent" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    //Maak rollen aan en stop het in de database.
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            //Hier wordt de super user aangemaakt - Beheer user van de applicatie
            var poweruser = new IdentityUser
            {
                UserName = Configuration["AppSettings:UserName"],
                Email = Configuration["AppSettings:UserEmail"],
            };

            //Hier wordt de moderator aangemaakt
            var moderator_user = new Moderator()
            {
                UserName = Configuration["AppSettings:ModeratorName"],
                Email = Configuration["AppSettings:ModeratorEmail"],
                Wachtwoord = "#1Geheim",
            };

            //Hier wordt de hulpverlener aangemaakt
            var hulpverlener_user = new Hulpverlener()
            {
                UserName = Configuration["AppSettings:HulpverlenerName"],
                Email = Configuration["AppSettings:HulpverlenerEmail"],
                Specialisme = Configuration["AppSettings:HulpverlenerSpecialisme"],
                Naam = Configuration["AppSettings:HulpverlenerNaam"],
                Achternaam = Configuration["AppSettings:HulpverlenerAchternaam"],


            };

            //Hier wordt de hulpverlener aangemaakt
            var hulpverlener_user2 = new Hulpverlener()
            {
                UserName = Configuration["AppSettings:HulpverlenerName2"],
                Email = Configuration["AppSettings:HulpverlenerEmail2"],
                Specialisme = Configuration["AppSettings:HulpverlenerSpecialisme2"],
                Naam = Configuration["AppSettings:HulpverlenerNaam2"],
                Achternaam = Configuration["AppSettings:HulpverlenerAchternaam2"],

            };

            //Hier wordt de client aangemaakt
            var client_user = new Client
            {
                UserName = Configuration["AppSettings:ClientName"],
                Email = Configuration["AppSettings:ClientEmail"],
                HulpverlenerId = Guid.Parse(hulpverlener_user.Id),
                Nicknaam = Configuration["AppSettings:ClientNickname"],
                LeeftijdsCategorie = Configuration["AppSettings:ClientLeeftijdsCategorie"],
                Naam = Configuration["AppSettings:ClientName"],
                Achternaam = Configuration["AppSettings:ClientAchternaam"],
                Geboortedatum = DateTime.Parse(Configuration["AppSettings:ClientGeboortedatum"]),
                isKindAccount = Boolean.Parse(Configuration["AppSettings:ClientIsKindAccount"]),
            };

            //Hier wordt de client aangemaakt
            var client_user2 = new Client
            {
                UserName = Configuration["AppSettings:ClientName2"],
                Email = Configuration["AppSettings:ClientEmail2"],
                HulpverlenerId = Guid.Parse(hulpverlener_user2.Id),
                Nicknaam = Configuration["AppSettings:ClientNickname2"],
                LeeftijdsCategorie = Configuration["AppSettings:ClientLeeftijdsCategorie2"],
                Naam = Configuration["AppSettings:ClientName2"],
                Achternaam = Configuration["AppSettings:ClientAchternaam2"],
                Geboortedatum = DateTime.Parse(Configuration["AppSettings:ClientGeboortedatum2"]),
                isKindAccount = Boolean.Parse(Configuration["AppSettings:ClientIsKindAccount2"]),
            };


            //Ensure you have these values in your appsettings.json file
            string userPWD = Configuration["AppSettings:UserPassword"];

            var _user = await UserManager.FindByEmailAsync(Configuration["AppSettings:AdminUserEmail"]);
            var _moderator = await UserManager.FindByEmailAsync(Configuration["AppSettings:ModeratorEmail"]);
            var _hulpverlener = await UserManager.FindByEmailAsync(Configuration["AppSettings:HulpverlenerEmail"]);
            var _hulpverlener2 = await UserManager.FindByEmailAsync(Configuration["AppSettings:HulpverlenerEmail2"]);
            var _client = await UserManager.FindByEmailAsync(Configuration["AppSettings:ClientEmail"]);
            var _client2 = await UserManager.FindByEmailAsync(Configuration["AppSettings:ClientEmail2"]);

            if (_user == null)
            {
                var createPowerUser = await UserManager.CreateAsync(poweruser, userPWD);
                if (createPowerUser.Succeeded)
                {
                    //here we tie the new user to the role
                    await UserManager.AddToRoleAsync(poweruser, "Admin");

                }
            }

            if (_moderator == null)
            {
                var createModeratorUser = await UserManager.CreateAsync(moderator_user, userPWD);
                if (createModeratorUser.Succeeded)
                {
                    //here we tie the new user to the role
                    await UserManager.AddToRoleAsync(moderator_user, "Moderator");

                }
            }

            if (_hulpverlener == null)
            {
                var createHulpverlenerUser = await UserManager.CreateAsync(hulpverlener_user, userPWD);
                if (createHulpverlenerUser.Succeeded)
                {
                    //here we tie the new user to the role
                    await UserManager.AddToRoleAsync(hulpverlener_user, "Hulpverlener");

                }
            }

            if (_hulpverlener2 == null)
            {
                var createHulpverlenerUser = await UserManager.CreateAsync(hulpverlener_user2, userPWD);
                if (createHulpverlenerUser.Succeeded)
                {
                    //here we tie the new user to the role
                    await UserManager.AddToRoleAsync(hulpverlener_user2, "Hulpverlener");

                }
            }

            if (_client == null)
            {
                var createClientUser = await UserManager.CreateAsync(client_user, userPWD);
                if (createClientUser.Succeeded)
                {
                    //here we tie the new user to the role
                    await UserManager.AddToRoleAsync(client_user, "Client");

                }
            }
            if (_client2 == null)
            {
                var createClientUser = await UserManager.CreateAsync(client_user2, userPWD);
                if (createClientUser.Succeeded)
                {
                    //here we tie the new user to the role
                    await UserManager.AddToRoleAsync(client_user2, "Client");

                }
            }
        }
    }
}

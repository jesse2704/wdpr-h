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

namespace wdpr_h
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            //Maak custom rollen aan including een super user
            //CreateRoles(serviceProvider).GetAwaiter().GetResult();
        }

        private async Task CreateRoles(IServiceProvider serviceProvider)
        {
            //Initialiseer custom rollen
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            string[] roleNames = { "Admin", "Moderator", "Client", "Hulpverlener" };
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
            var moderator_user = new IdentityUser
            {
                UserName = Configuration["AppSettings:ModeratorName"],
                Email = Configuration["AppSettings:ModeratorEmail"],
            };

            //Hier wordt de moderator aangemaakt
            var hulpverlener_user = new IdentityUser
            {
                UserName = Configuration["AppSettings:HulpverlenerName"],
                Email = Configuration["AppSettings:HulpverlenerEmail"],
            };

             //Hier wordt de Client aangemaakt
            var client_user = new IdentityUser
            {
                UserName = Configuration["AppSettings:ClientName"],
                Email = Configuration["AppSettings:ClientEmail"],
            };

            //Ensure you have these values in your appsettings.json file
            string userPWD = Configuration["AppSettings:UserPassword"];

            var _user = await UserManager.FindByEmailAsync(Configuration["AppSettings:AdminUserEmail"]);
            var _moderator = await UserManager.FindByEmailAsync(Configuration["AppSettings:ModeratorEmail"]);
            var _hulpverlener = await UserManager.FindByEmailAsync(Configuration["AppSettings:HulpverlenerEmail"]);
            var _client = await UserManager.FindByEmailAsync(Configuration["AppSettings:ClientEmail"]);

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

            if (_client == null)
            {
                var createClientUser = await UserManager.CreateAsync(client_user, userPWD);
                if (createClientUser.Succeeded)
                {
                    //here we tie the new user to the role
                    await UserManager.AddToRoleAsync(client_user, "Client");

                }
            }
        }
    }
}

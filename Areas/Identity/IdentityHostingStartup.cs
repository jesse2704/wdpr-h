using System;
using Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(wdpr_h.Areas.Identity.IdentityHostingStartup))]
namespace wdpr_h.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                // services.AddDbContext<ApplicationDbContext>(options =>
                //     options.UseSqlite(
                //         context.Configuration.GetConnectionString("ApplicationDbContextConnection")));

                // services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                //     .AddEntityFrameworkStores<ApplicationDbContext>();
            });
        }
    }
}
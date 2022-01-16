using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using wdpr_h.Models;

namespace wdpr_h.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<wdpr_h.Models.Hulpverlener> Hulpverlener { get; set; }

        public DbSet<wdpr_h.Models.Zelfhulpgroep> Zelfhulpgroep { get; set; }

        public DbSet<wdpr_h.Models.Moderator> Moderator { get; set; }

        public DbSet<wdpr_h.Models.Chatrestrictie> Chatrestrictie { get; set; }

        public DbSet<wdpr_h.Models.ClientZelfhulpgroep> ClientZelfhulpgroep { get; set; }

        public DbSet<wdpr_h.Models.Aanmeld> Aanmeld { get; set; }

        public DbSet<wdpr_h.Models.Client> Client { get; set; }
    }
}

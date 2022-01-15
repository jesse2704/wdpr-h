using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using wdpr_h.Models;

    public class MyContext : DbContext
    {
        public MyContext (DbContextOptions<MyContext> options)
            : base(options)
        {
        }

        public DbSet<wdpr_h.Models.Hulpverlener> Hulpverlener { get; set; }

        public DbSet<wdpr_h.Models.Zelfhulpgroep> Zelfhulpgroep { get; set; }

        public DbSet<wdpr_h.Models.Moderator> Moderator { get; set; }

        public DbSet<wdpr_h.Models.Chatrestrictie> Chatrestrictie { get; set; }

        public DbSet<wdpr_h.Models.ClientZelfhulpgroep> ClientZelfhulpgroep { get; set; }

        public DbSet<wdpr_h.Models.Client> Client { get; set; }

        public DbSet<wdpr_h.Models.Aanmeld> Aanmeld { get; set; }
    }

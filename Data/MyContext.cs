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

        public DbSet<wdpr_h.Models.Client> Client { get; set; }

        public DbSet<wdpr_h.Models.Zelfhulpgroep> Zelfhulpgroep { get; set; }
    }

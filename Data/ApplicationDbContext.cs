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




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Zelfhulpgroep>().HasData(
                new Zelfhulpgroep { Id = Guid.NewGuid(), Onderwerp = "Autisme", Titel = "Groep 1", LeeftijdsCategorie = "18"},
                new Zelfhulpgroep { Id = Guid.NewGuid(), Onderwerp = "Depressie", Titel = "Groep 2", LeeftijdsCategorie = "17"},
                new Zelfhulpgroep { Id = Guid.NewGuid(), Onderwerp = "Hooggevoeligheid", Titel = "Groep 3", LeeftijdsCategorie = "16"} 
            );
        }

    }

}

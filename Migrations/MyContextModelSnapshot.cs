﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace wdpr_h.Migrations
{
    [DbContext(typeof(MyContext))]
    partial class MyContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.9");

            modelBuilder.Entity("wdpr_h.Models.Aanmeld", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Aandoening")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("TEXT");

                    b.Property<string>("Achternaam")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Geboortedatum")
                        .HasColumnType("TEXT");

                    b.Property<string>("Naam")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("TEXT");

                    b.Property<bool>("heeftVerwijzing")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("hulpVerlenerId")
                        .HasColumnType("TEXT");

                    b.Property<bool>("voorKind")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Aanmeld");
                });

            modelBuilder.Entity("wdpr_h.Models.Chatrestrictie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("BeginTijd")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("EindTijd")
                        .HasColumnType("TEXT");

                    b.Property<string>("Reden")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<Guid>("User")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ZelfhulpgroepId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Chatrestrictie");
                });

            modelBuilder.Entity("wdpr_h.Models.Client", b =>
                {
                    b.Property<Guid>("User")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Achternaam")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<int>("HulpverlenerId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LeeftijdsCategorie")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Naam")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("TEXT");

                    b.Property<string>("Nicknaam")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("TEXT");

                    b.Property<Guid>("OuderAccount")
                        .HasColumnType("TEXT");

                    b.Property<string>("Wachtwoord")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("isKindAccount")
                        .HasColumnType("INTEGER");

                    b.HasKey("User");

                    b.ToTable("Client");
                });

            modelBuilder.Entity("wdpr_h.Models.ClientZelfhulpgroep", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("IdClient")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("IdGroep")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("ClientZelfhulpgroep");
                });

            modelBuilder.Entity("wdpr_h.Models.Hulpverlener", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Achternaam")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("TEXT");

                    b.Property<string>("Naam")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("TEXT");

                    b.Property<string>("Specialisme")
                        .IsRequired()
                        .HasMaxLength(35)
                        .HasColumnType("TEXT");

                    b.Property<string>("Wachtwoord")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Hulpverlener");
                });

            modelBuilder.Entity("wdpr_h.Models.Moderator", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Naam")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("TEXT");

                    b.Property<string>("Wachtwoord")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Moderator");
                });

            modelBuilder.Entity("wdpr_h.Models.Zelfhulpgroep", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("LeeftijdsCategorie")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Onderwerp")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("TEXT");

                    b.Property<string>("Titel")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Zelfhulpgroep");
                });
#pragma warning restore 612, 618
        }
    }
}
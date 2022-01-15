using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace wdpr_h.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Aanmeld",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Naam = table.Column<string>(type: "TEXT", maxLength: 25, nullable: false),
                    Achternaam = table.Column<string>(type: "TEXT", maxLength: 25, nullable: false),
                    Geboortedatum = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Aandoening = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    hulpVerlenerId = table.Column<Guid>(type: "TEXT", nullable: false),
                    voorKind = table.Column<bool>(type: "INTEGER", nullable: false),
                    heeftVerwijzing = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aanmeld", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Chatrestrictie",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    User = table.Column<Guid>(type: "TEXT", nullable: false),
                    BeginTijd = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EindTijd = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Reden = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    ZelfhulpgroepId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chatrestrictie", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    User = table.Column<Guid>(type: "TEXT", nullable: false),
                    Nicknaam = table.Column<string>(type: "TEXT", maxLength: 15, nullable: false),
                    LeeftijdsCategorie = table.Column<string>(type: "TEXT", nullable: false),
                    Naam = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    Achternaam = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    isKindAccount = table.Column<bool>(type: "INTEGER", nullable: false),
                    OuderAccount = table.Column<Guid>(type: "TEXT", nullable: false),
                    HulpverlenerId = table.Column<int>(type: "INTEGER", nullable: false),
                    Wachtwoord = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.User);
                });

            migrationBuilder.CreateTable(
                name: "ClientZelfhulpgroep",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IdClient = table.Column<Guid>(type: "TEXT", nullable: false),
                    IdGroep = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientZelfhulpgroep", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Hulpverlener",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Naam = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    Achternaam = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    Specialisme = table.Column<string>(type: "TEXT", maxLength: 35, nullable: false),
                    Wachtwoord = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hulpverlener", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Moderator",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Naam = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    Wachtwoord = table.Column<string>(type: "TEXT", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Moderator", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Zelfhulpgroep",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Titel = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Onderwerp = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    LeeftijdsCategorie = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zelfhulpgroep", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Aanmeld");

            migrationBuilder.DropTable(
                name: "Chatrestrictie");

            migrationBuilder.DropTable(
                name: "Client");

            migrationBuilder.DropTable(
                name: "ClientZelfhulpgroep");

            migrationBuilder.DropTable(
                name: "Hulpverlener");

            migrationBuilder.DropTable(
                name: "Moderator");

            migrationBuilder.DropTable(
                name: "Zelfhulpgroep");
        }
    }
}

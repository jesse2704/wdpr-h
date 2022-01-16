using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace wdpr_h.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Achternaam",
                table: "AspNetUsers",
                type: "TEXT",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "HulpverlenerId",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Hulpverlener_Achternaam",
                table: "AspNetUsers",
                type: "TEXT",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Hulpverlener_Naam",
                table: "AspNetUsers",
                type: "TEXT",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Hulpverlener_Wachtwoord",
                table: "AspNetUsers",
                type: "TEXT",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LeeftijdsCategorie",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Moderator_Naam",
                table: "AspNetUsers",
                type: "TEXT",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Moderator_Wachtwoord",
                table: "AspNetUsers",
                type: "TEXT",
                maxLength: 40,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Naam",
                table: "AspNetUsers",
                type: "TEXT",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nicknaam",
                table: "AspNetUsers",
                type: "TEXT",
                maxLength: 15,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OuderAccount",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Specialisme",
                table: "AspNetUsers",
                type: "TEXT",
                maxLength: 35,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Wachtwoord",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isKindAccount",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: true);

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
                name: "ClientZelfhulpgroep");

            migrationBuilder.DropTable(
                name: "Zelfhulpgroep");

            migrationBuilder.DropColumn(
                name: "Achternaam",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "HulpverlenerId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Hulpverlener_Achternaam",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Hulpverlener_Naam",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Hulpverlener_Wachtwoord",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LeeftijdsCategorie",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Moderator_Naam",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Moderator_Wachtwoord",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Naam",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Nicknaam",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "OuderAccount",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Specialisme",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Wachtwoord",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "isKindAccount",
                table: "AspNetUsers");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Friensify.Migrations
{
    public partial class VisitasPerfil : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VisitasPerfil",
                columns: table => new
                {
                    IdUsuario = table.Column<string>(nullable: false),
                    Fecha = table.Column<DateTime>(nullable: false),
                    Visitas = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisitasPerfil", x => new { x.IdUsuario, x.Fecha });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VisitasPerfil");
        }
    }
}

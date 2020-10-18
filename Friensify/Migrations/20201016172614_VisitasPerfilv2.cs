using Microsoft.EntityFrameworkCore.Migrations;

namespace Friensify.Migrations
{
    public partial class VisitasPerfilv2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Nombre",
                table: "VisitasPerfil",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nombre",
                table: "VisitasPerfil");
        }
    }
}

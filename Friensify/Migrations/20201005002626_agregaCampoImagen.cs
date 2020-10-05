using Microsoft.EntityFrameworkCore.Migrations;

namespace Friensify.Migrations
{
    public partial class agregaCampoImagen : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagenPerfil",
                table: "User",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagenPerfil",
                table: "User");
        }
    }
}

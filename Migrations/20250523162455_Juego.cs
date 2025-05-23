using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VideojuegosApp.Migrations
{
    /// <inheritdoc />
    public partial class Juego : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Precio",
                table: "Juegos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RutaImagen",
                table: "Juegos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Stock",
                table: "Juegos",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Precio",
                table: "Juegos");

            migrationBuilder.DropColumn(
                name: "RutaImagen",
                table: "Juegos");

            migrationBuilder.DropColumn(
                name: "Stock",
                table: "Juegos");
        }
    }
}

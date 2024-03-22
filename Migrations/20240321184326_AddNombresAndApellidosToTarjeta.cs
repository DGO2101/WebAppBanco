using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAppBanco4.Migrations
{
    /// <inheritdoc />
    public partial class AddNombresAndApellidosToTarjeta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Apellidos",
                table: "Tarjetas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Nombres",
                table: "Tarjetas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Apellidos",
                table: "Tarjetas");

            migrationBuilder.DropColumn(
                name: "Nombres",
                table: "Tarjetas");
        }
    }
}

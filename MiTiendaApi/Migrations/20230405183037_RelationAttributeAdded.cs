using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiTiendaApi.Migrations
{
    /// <inheritdoc />
    public partial class RelationAttributeAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Stock",
                table: "Productos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Cantidad",
                table: "ClientesProductos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Stock",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "Cantidad",
                table: "ClientesProductos");
        }
    }
}

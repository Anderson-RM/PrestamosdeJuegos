using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PrestamosdeJuegos.Migrations
{
    public partial class Migracion_Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Amigos",
                columns: table => new
                {
                    AmigoId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NombreCompleto = table.Column<string>(nullable: true),
                    Direccion = table.Column<string>(nullable: true),
                    Telefono = table.Column<long>(nullable: false),
                    Celular = table.Column<long>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    FechaNacimiento = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Amigos", x => x.AmigoId);
                });

            migrationBuilder.CreateTable(
                name: "Juegos",
                columns: table => new
                {
                    JuegoId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FechaCompra = table.Column<DateTime>(nullable: false),
                    Descripcion = table.Column<string>(nullable: true),
                    Precio = table.Column<double>(nullable: false),
                    Existencia = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Juegos", x => x.JuegoId);
                });

            migrationBuilder.CreateTable(
                name: "Prestamos",
                columns: table => new
                {
                    PrestamoId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Fecha = table.Column<DateTime>(nullable: false),
                    AmigoId = table.Column<int>(nullable: false),
                    Observacion = table.Column<string>(nullable: true),
                    CantidadJuegosTotal = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prestamos", x => x.PrestamoId);
                    table.ForeignKey(
                        name: "FK_Prestamos_Amigos_AmigoId",
                        column: x => x.AmigoId,
                        principalTable: "Amigos",
                        principalColumn: "AmigoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EntradasJuegos",
                columns: table => new
                {
                    EntradaJuegoId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Fecha = table.Column<DateTime>(nullable: false),
                    JuegoId = table.Column<int>(nullable: false),
                    Cantidad = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntradasJuegos", x => x.EntradaJuegoId);
                    table.ForeignKey(
                        name: "FK_EntradasJuegos_Juegos_JuegoId",
                        column: x => x.JuegoId,
                        principalTable: "Juegos",
                        principalColumn: "JuegoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrestamosDetalle",
                columns: table => new
                {
                    PrestamoDetalleId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PrestamosId = table.Column<int>(nullable: false),
                    JuegoId = table.Column<int>(nullable: false),
                    CantidadJuegos = table.Column<double>(nullable: false),
                    PrestamoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrestamosDetalle", x => x.PrestamoDetalleId);
                    table.ForeignKey(
                        name: "FK_PrestamosDetalle_Juegos_JuegoId",
                        column: x => x.JuegoId,
                        principalTable: "Juegos",
                        principalColumn: "JuegoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PrestamosDetalle_Prestamos_PrestamoId",
                        column: x => x.PrestamoId,
                        principalTable: "Prestamos",
                        principalColumn: "PrestamoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EntradasJuegos_JuegoId",
                table: "EntradasJuegos",
                column: "JuegoId");

            migrationBuilder.CreateIndex(
                name: "IX_Prestamos_AmigoId",
                table: "Prestamos",
                column: "AmigoId");

            migrationBuilder.CreateIndex(
                name: "IX_PrestamosDetalle_JuegoId",
                table: "PrestamosDetalle",
                column: "JuegoId");

            migrationBuilder.CreateIndex(
                name: "IX_PrestamosDetalle_PrestamoId",
                table: "PrestamosDetalle",
                column: "PrestamoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EntradasJuegos");

            migrationBuilder.DropTable(
                name: "PrestamosDetalle");

            migrationBuilder.DropTable(
                name: "Juegos");

            migrationBuilder.DropTable(
                name: "Prestamos");

            migrationBuilder.DropTable(
                name: "Amigos");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Persona",
                columns: table => new
                {
                    IdPersona = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombres = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Genero = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Edad = table.Column<int>(type: "int", nullable: false),
                    Identificacion = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClienteId = table.Column<int>(type: "int", nullable: true),
                    Contrasenia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Estado = table.Column<bool>(type: "bit", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persona", x => x.IdPersona);
                });

            migrationBuilder.CreateTable(
                name: "Cuenta",
                columns: table => new
                {
                    NumeroCuenta = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TipoCuenta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SaldoInicial = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SaldoActual = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false),
                    ClientePersonaFKIdPersona = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cuenta", x => x.NumeroCuenta);
                    table.ForeignKey(
                        name: "FK_Cuenta_Persona_ClientePersonaFKIdPersona",
                        column: x => x.ClientePersonaFKIdPersona,
                        principalTable: "Persona",
                        principalColumn: "IdPersona",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Movimientos",
                columns: table => new
                {
                    IdMovimientos = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TipoMovimiento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Saldo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SaldoInicial = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CuentaNumeroCuenta = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movimientos", x => x.IdMovimientos);
                    table.ForeignKey(
                        name: "FK_Movimientos_Cuenta_CuentaNumeroCuenta",
                        column: x => x.CuentaNumeroCuenta,
                        principalTable: "Cuenta",
                        principalColumn: "NumeroCuenta",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cuenta_ClientePersonaFKIdPersona",
                table: "Cuenta",
                column: "ClientePersonaFKIdPersona");

            migrationBuilder.CreateIndex(
                name: "IX_Cuenta_NumeroCuenta",
                table: "Cuenta",
                column: "NumeroCuenta",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Movimientos_CuentaNumeroCuenta",
                table: "Movimientos",
                column: "CuentaNumeroCuenta");

            migrationBuilder.CreateIndex(
                name: "IX_Persona_Identificacion",
                table: "Persona",
                column: "Identificacion",
                unique: true,
                filter: "[Identificacion] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Movimientos");

            migrationBuilder.DropTable(
                name: "Cuenta");

            migrationBuilder.DropTable(
                name: "Persona");
        }
    }
}

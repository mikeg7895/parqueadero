using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Parqueadero.Migrations
{
    /// <inheritdoc />
    public partial class MoreModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tarifa_Parqueaderos_ParqueaderoId",
                table: "Tarifa");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tarifa",
                table: "Tarifa");

            migrationBuilder.RenameTable(
                name: "Tarifa",
                newName: "Tarifas");

            migrationBuilder.RenameIndex(
                name: "IX_Tarifa_ParqueaderoId",
                table: "Tarifas",
                newName: "IX_Tarifas_ParqueaderoId");

            migrationBuilder.AddColumn<int>(
                name: "PlanEspecialId",
                table: "Usuarios",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ParqueaderoId",
                table: "Tarifas",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tarifas",
                table: "Tarifas",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Descuentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    Porcentaje = table.Column<decimal>(type: "numeric", nullable: false),
                    Codigo = table.Column<string>(type: "text", nullable: false),
                    FechaExpiracion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Descuentos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlanesEspeciales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: false),
                    Costo = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanesEspeciales", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reservas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ZonaId = table.Column<int>(type: "integer", nullable: false),
                    UsuarioId = table.Column<int>(type: "integer", nullable: false),
                    VehiculoId = table.Column<int>(type: "integer", nullable: false),
                    HoraEntrada = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    HoraSalida = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Estado = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vehiculos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Placa = table.Column<string>(type: "text", nullable: false),
                    UsuarioId = table.Column<int>(type: "integer", nullable: false),
                    TipoVehiculo = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehiculos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cobros",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ReservaId = table.Column<int>(type: "integer", nullable: false),
                    TarifaId = table.Column<int>(type: "integer", nullable: false),
                    DescuentoId = table.Column<int>(type: "integer", nullable: false),
                    Total = table.Column<decimal>(type: "numeric", nullable: false),
                    FechaCobro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cobros", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cobros_Descuentos_DescuentoId",
                        column: x => x.DescuentoId,
                        principalTable: "Descuentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cobros_Reservas_ReservaId",
                        column: x => x.ReservaId,
                        principalTable: "Reservas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cobros_Tarifas_TarifaId",
                        column: x => x.TarifaId,
                        principalTable: "Tarifas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Recibos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CobroId = table.Column<int>(type: "integer", nullable: false),
                    Codigo = table.Column<string>(type: "text", nullable: false),
                    FechaEmision = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Enviado = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recibos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recibos_Cobros_CobroId",
                        column: x => x.CobroId,
                        principalTable: "Cobros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                column: "PlanEspecialId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Cobros_DescuentoId",
                table: "Cobros",
                column: "DescuentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Cobros_ReservaId",
                table: "Cobros",
                column: "ReservaId");

            migrationBuilder.CreateIndex(
                name: "IX_Cobros_TarifaId",
                table: "Cobros",
                column: "TarifaId");

            migrationBuilder.CreateIndex(
                name: "IX_Recibos_CobroId",
                table: "Recibos",
                column: "CobroId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tarifas_Parqueaderos_ParqueaderoId",
                table: "Tarifas",
                column: "ParqueaderoId",
                principalTable: "Parqueaderos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tarifas_Parqueaderos_ParqueaderoId",
                table: "Tarifas");

            migrationBuilder.DropTable(
                name: "PlanesEspeciales");

            migrationBuilder.DropTable(
                name: "Recibos");

            migrationBuilder.DropTable(
                name: "Vehiculos");

            migrationBuilder.DropTable(
                name: "Cobros");

            migrationBuilder.DropTable(
                name: "Descuentos");

            migrationBuilder.DropTable(
                name: "Reservas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tarifas",
                table: "Tarifas");

            migrationBuilder.DropColumn(
                name: "PlanEspecialId",
                table: "Usuarios");

            migrationBuilder.RenameTable(
                name: "Tarifas",
                newName: "Tarifa");

            migrationBuilder.RenameIndex(
                name: "IX_Tarifas_ParqueaderoId",
                table: "Tarifa",
                newName: "IX_Tarifa_ParqueaderoId");

            migrationBuilder.AlterColumn<int>(
                name: "ParqueaderoId",
                table: "Tarifa",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tarifa",
                table: "Tarifa",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tarifa_Parqueaderos_ParqueaderoId",
                table: "Tarifa",
                column: "ParqueaderoId",
                principalTable: "Parqueaderos",
                principalColumn: "Id");
        }
    }
}

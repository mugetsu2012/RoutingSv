using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RuteoPedidos.Data.Migrations
{
    public partial class MigracionInicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cuenta",
                columns: table => new
                {
                    Codigo = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreCuenta = table.Column<string>(maxLength: 200, nullable: false),
                    NombreContacto = table.Column<string>(maxLength: 200, nullable: false),
                    TelefonoContacto = table.Column<string>(maxLength: 200, nullable: false),
                    Activo = table.Column<bool>(nullable: false),
                    FechaIngreso = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cuenta", x => x.Codigo);
                });

            migrationBuilder.CreateTable(
                name: "Destino",
                columns: table => new
                {
                    Codigo = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCuenta = table.Column<int>(nullable: false),
                    Nombre = table.Column<string>(maxLength: 1000, nullable: false),
                    FechaIngreso = table.Column<DateTimeOffset>(nullable: false),
                    LatitudUbicacion = table.Column<decimal>(type: "decimal(9,6)", nullable: false),
                    LongitudUbicacion = table.Column<decimal>(type: "decimal(9,6)", nullable: false),
                    Direccion = table.Column<string>(nullable: true),
                    TelefonoContacto = table.Column<string>(maxLength: 100, nullable: true),
                    Activo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Destino", x => x.Codigo);
                    table.ForeignKey(
                        name: "FK_Destino_Cuenta_IdCuenta",
                        column: x => x.IdCuenta,
                        principalTable: "Cuenta",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    IdUsuario = table.Column<string>(maxLength: 100, nullable: false),
                    Nombre = table.Column<string>(maxLength: 200, nullable: false),
                    Apellido = table.Column<string>(maxLength: 200, nullable: false),
                    Password = table.Column<byte[]>(nullable: true),
                    Email = table.Column<string>(maxLength: 200, nullable: false),
                    FechaIngreso = table.Column<DateTimeOffset>(nullable: false),
                    IdCuenta = table.Column<int>(nullable: false),
                    Activo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.IdUsuario);
                    table.ForeignKey(
                        name: "FK_Usuario_Cuenta_IdCuenta",
                        column: x => x.IdCuenta,
                        principalTable: "Cuenta",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Motorista",
                columns: table => new
                {
                    Codigo = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCuenta = table.Column<int>(nullable: false),
                    FechaIngreso = table.Column<DateTimeOffset>(nullable: false),
                    LatitudUltimaUbicacion = table.Column<decimal>(type: "decimal(9,6)", nullable: true),
                    LongitudUltimaUbicacion = table.Column<decimal>(type: "decimal(9,6)", nullable: true),
                    FechaActualizacionUbicacion = table.Column<DateTimeOffset>(nullable: true),
                    NombreCompleto = table.Column<string>(maxLength: 200, nullable: false),
                    TelefonoContacto = table.Column<string>(maxLength: 100, nullable: false),
                    PlacasVehiculo = table.Column<string>(maxLength: 100, nullable: false),
                    TipoVehiculo = table.Column<int>(nullable: false),
                    IdUsuario = table.Column<string>(maxLength: 100, nullable: true),
                    TextoTipoVehiculo = table.Column<string>(maxLength: 100, nullable: false),
                    Activo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Motorista", x => x.Codigo);
                    table.ForeignKey(
                        name: "FK_Motorista_Cuenta_IdCuenta",
                        column: x => x.IdCuenta,
                        principalTable: "Cuenta",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Motorista_Usuario_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuario",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HistoricoTareasOrdenamiento",
                columns: table => new
                {
                    Codigo = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdMotorista = table.Column<int>(nullable: false),
                    CodigoTareasInvolucradas = table.Column<string>(nullable: true),
                    FechaIngreso = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoricoTareasOrdenamiento", x => x.Codigo);
                    table.ForeignKey(
                        name: "FK_HistoricoTareasOrdenamiento_Motorista_IdMotorista",
                        column: x => x.IdMotorista,
                        principalTable: "Motorista",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HistoricoUbicacionMotorista",
                columns: table => new
                {
                    Codigo = table.Column<string>(maxLength: 100, nullable: false),
                    IdMotorista = table.Column<int>(nullable: false),
                    Longitud = table.Column<decimal>(type: "decimal(9,6)", nullable: false),
                    Latitud = table.Column<decimal>(type: "decimal(9,6)", nullable: false),
                    FechaRegistroUbicacion = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoricoUbicacionMotorista", x => x.Codigo);
                    table.ForeignKey(
                        name: "FK_HistoricoUbicacionMotorista_Motorista_IdMotorista",
                        column: x => x.IdMotorista,
                        principalTable: "Motorista",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tarea",
                columns: table => new
                {
                    Codigo = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCuenta = table.Column<int>(nullable: false),
                    DestinoCliente = table.Column<string>(maxLength: 1000, nullable: false),
                    LatitudUbicacion = table.Column<decimal>(type: "decimal(9,6)", nullable: false),
                    LongitudUbicacion = table.Column<decimal>(type: "decimal(9,6)", nullable: false),
                    Indicaciones = table.Column<string>(nullable: true),
                    TelefonoContacto = table.Column<string>(maxLength: 100, nullable: true),
                    DetalleTarea = table.Column<string>(nullable: false),
                    FechaIngreso = table.Column<DateTimeOffset>(nullable: false),
                    IdClienteUtilizado = table.Column<int>(nullable: true),
                    EstadoTarea = table.Column<int>(nullable: false),
                    TextoEstadoTarea = table.Column<string>(maxLength: 100, nullable: false),
                    FechaUltimoCambioEstado = table.Column<DateTimeOffset>(nullable: true),
                    IdMotoristaAsignado = table.Column<int>(nullable: true),
                    FechaAsignacionMotorista = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tarea", x => x.Codigo);
                    table.ForeignKey(
                        name: "FK_Tarea_Destino_IdClienteUtilizado",
                        column: x => x.IdClienteUtilizado,
                        principalTable: "Destino",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tarea_Cuenta_IdCuenta",
                        column: x => x.IdCuenta,
                        principalTable: "Cuenta",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tarea_Motorista_IdMotoristaAsignado",
                        column: x => x.IdMotoristaAsignado,
                        principalTable: "Motorista",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Visita",
                columns: table => new
                {
                    Codigo = table.Column<string>(maxLength: 100, nullable: false),
                    IdTarea = table.Column<int>(nullable: false),
                    ResultadoVisita = table.Column<int>(nullable: false),
                    TextoResultadoVisita = table.Column<string>(maxLength: 100, nullable: false),
                    FechaIngreso = table.Column<DateTimeOffset>(nullable: false),
                    Comentario = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visita", x => x.Codigo);
                    table.ForeignKey(
                        name: "FK_Visita_Tarea_IdTarea",
                        column: x => x.IdTarea,
                        principalTable: "Tarea",
                        principalColumn: "Codigo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Cuenta",
                columns: new[] { "Codigo", "Activo", "FechaIngreso", "NombreContacto", "NombreCuenta", "TelefonoContacto" },
                values: new object[] { 1, true, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Administrador", "Cuenta de pruebas", "22231203" });

            migrationBuilder.InsertData(
                table: "Motorista",
                columns: new[] { "Codigo", "Activo", "FechaActualizacionUbicacion", "FechaIngreso", "IdCuenta", "IdUsuario", "LatitudUltimaUbicacion", "LongitudUltimaUbicacion", "NombreCompleto", "PlacasVehiculo", "TelefonoContacto", "TextoTipoVehiculo", "TipoVehiculo" },
                values: new object[,]
                {
                    { 1, true, null, new DateTimeOffset(new DateTime(2020, 5, 27, 23, 7, 9, 837, DateTimeKind.Unspecified).AddTicks(7994), new TimeSpan(0, -6, 0, 0, 0)), 1, null, null, null, "Juan Perez", "P8984565", "62789845", "CarroParticular", 1 },
                    { 2, true, new DateTimeOffset(new DateTime(2020, 5, 27, 23, 7, 9, 842, DateTimeKind.Unspecified).AddTicks(9835), new TimeSpan(0, -6, 0, 0, 0)), new DateTimeOffset(new DateTime(2020, 5, 27, 23, 7, 9, 842, DateTimeKind.Unspecified).AddTicks(9583), new TimeSpan(0, -6, 0, 0, 0)), 1, null, 13.70383m, -89.24804m, "Lucas Molina", "P523478", "62785545", "CarroParticular", 1 }
                });

            migrationBuilder.InsertData(
                table: "Tarea",
                columns: new[] { "Codigo", "DestinoCliente", "DetalleTarea", "EstadoTarea", "FechaAsignacionMotorista", "FechaIngreso", "FechaUltimoCambioEstado", "IdClienteUtilizado", "IdCuenta", "IdMotoristaAsignado", "Indicaciones", "LatitudUbicacion", "LongitudUbicacion", "TelefonoContacto", "TextoEstadoTarea" },
                values: new object[,]
                {
                    { 1, "Casa numero 5, Colonia Escalon", "Llega a Bitworks y entregar una Pizza", 0, null, new DateTimeOffset(new DateTime(2020, 5, 23, 15, 10, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, 1, null, null, 13.70672m, -89.26071m, "22224578", "Pendiente" },
                    { 2, "Casa Juana en Residencial Sausalito", "Tocar la puerta en el porton verde, preguntar por Juana", 0, null, new DateTimeOffset(new DateTime(2020, 5, 22, 9, 5, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, 1, null, "Sarita, Calle Del Mirador, Residencial Sausalito, Colonia Escalón, Distrito Municipal 3, San Salvador", 13.70914m, -89.24305m, "65456912", "Pendiente" },
                    { 3, "Tienda el Pinalito, Tecla", "Llegar a la tienda, entregar las pupusas", 0, null, new DateTimeOffset(new DateTime(2020, 5, 23, 12, 45, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, 1, null, "Tienda El Pinalito, Calle Jose Ciriaco López, Barrio El Centro, Santa Tecla, La Libertad, 1501, El Salvador", 13.67447m, -89.24305m, "74123697", "Pendiente" }
                });

            migrationBuilder.InsertData(
                table: "Usuario",
                columns: new[] { "IdUsuario", "Activo", "Apellido", "Email", "FechaIngreso", "IdCuenta", "Nombre", "Password" },
                values: new object[] { "admin", true, "Magic", "alexanderortiz333@gmail.com", new DateTimeOffset(new DateTime(2020, 5, 27, 23, 7, 9, 847, DateTimeKind.Unspecified).AddTicks(225), new TimeSpan(0, -6, 0, 0, 0)), 1, "Administrador", new byte[] { 233, 122, 94, 60, 23, 182, 90, 255, 14, 13, 91, 177, 163, 58, 136, 30 } });

            migrationBuilder.CreateIndex(
                name: "IX_Destino_IdCuenta",
                table: "Destino",
                column: "IdCuenta");

            migrationBuilder.CreateIndex(
                name: "IX_HistoricoTareasOrdenamiento_IdMotorista",
                table: "HistoricoTareasOrdenamiento",
                column: "IdMotorista");

            migrationBuilder.CreateIndex(
                name: "IX_HistoricoUbicacionMotorista_IdMotorista",
                table: "HistoricoUbicacionMotorista",
                column: "IdMotorista");

            migrationBuilder.CreateIndex(
                name: "IX_Motorista_IdCuenta",
                table: "Motorista",
                column: "IdCuenta");

            migrationBuilder.CreateIndex(
                name: "IX_Motorista_IdUsuario",
                table: "Motorista",
                column: "IdUsuario",
                unique: true,
                filter: "[IdUsuario] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Tarea_IdClienteUtilizado",
                table: "Tarea",
                column: "IdClienteUtilizado");

            migrationBuilder.CreateIndex(
                name: "IX_Tarea_IdCuenta",
                table: "Tarea",
                column: "IdCuenta");

            migrationBuilder.CreateIndex(
                name: "IX_Tarea_IdMotoristaAsignado",
                table: "Tarea",
                column: "IdMotoristaAsignado");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_IdCuenta",
                table: "Usuario",
                column: "IdCuenta");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_Email_IdCuenta",
                table: "Usuario",
                columns: new[] { "Email", "IdCuenta" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Visita_IdTarea",
                table: "Visita",
                column: "IdTarea");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistoricoTareasOrdenamiento");

            migrationBuilder.DropTable(
                name: "HistoricoUbicacionMotorista");

            migrationBuilder.DropTable(
                name: "Visita");

            migrationBuilder.DropTable(
                name: "Tarea");

            migrationBuilder.DropTable(
                name: "Destino");

            migrationBuilder.DropTable(
                name: "Motorista");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Cuenta");
        }
    }
}

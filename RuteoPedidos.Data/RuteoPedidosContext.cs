using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using RuteoPedidos.Core.Model;
using RuteoPedidos.Core.Model.Motoristas;
using RuteoPedidos.Core.Model.Tareas;

namespace RuteoPedidos.Data
{
    public class RuteoPedidosContext: DbContext
    {
        public RuteoPedidosContext(DbContextOptions<RuteoPedidosContext> options): base(options)
        {
        }

        public DbSet<Destino> Destinos { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Cuenta> Cuentas { get; set; }

        public DbSet<Visita> Visitas { get; set; }

        public DbSet<Tarea> Tareas { get; set; }

        public DbSet<Motorista> Motoristas { get; set; }

        public DbSet<HistoricoUbicacionMotorista> HistoricoUbicacionMotoristas { get; set; }

        public DbSet<HistoricoTareasOrdenamiento> HistoricoTareasOrdenamientos { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(RuteoPedidosContext).Assembly);

            #region Seed datos

            //Seed de la cuenta
            modelBuilder.Entity<Cuenta>().HasData(
                new Cuenta()
                {
                    NombreCuenta = "Cuenta de pruebas",
                    TelefonoContacto = "22231203",
                    NombreContacto = "Administrador",
                    Activo = true,
                    Codigo = 1
                });

            //Se crean un par de motoristas de prueba
            modelBuilder.Entity<Motorista>().HasData(
                new Motorista()
                {
                    IdCuenta = 1,
                    Codigo = 1,
                    TipoVehiculo = TipoVehiculo.CarroParticular,
                    TelefonoContacto = "62789845",
                    LongitudUltimaUbicacion = null,
                    LatitudUltimaUbicacion = null,
                    NombreCompleto = "Juan Perez",
                    Activo = true,
                    FechaActualizacionUbicacion = null,
                    PlacasVehiculo = "P8984565"
                },
                new Motorista()
                {
                    IdCuenta = 1,
                    Codigo = 2,
                    TipoVehiculo = TipoVehiculo.CarroParticular,
                    TelefonoContacto = "62785545",
                    LongitudUltimaUbicacion = -89.24804m,
                    LatitudUltimaUbicacion = 13.70383m,
                    NombreCompleto = "Lucas Molina",
                    Activo = true,
                    FechaActualizacionUbicacion = DateTimeOffset.Now,
                    PlacasVehiculo = "P523478"
                });


            modelBuilder.Entity<Tarea>().HasData(
                new Tarea()
                {
                    Codigo = 1,
                    TelefonoContacto = "22224578",
                    FechaIngreso = new DateTimeOffset(2020, 5, 23, 15, 10, 0, TimeSpan.Zero),
                    DestinoCliente = "Casa numero 5, Colonia Escalon",
                    LongitudUbicacion = -89.26071m,
                    LatitudUbicacion = 13.70672m,
                    IdCuenta = 1,
                    DetalleTarea = "Llega a Bitworks y entregar una Pizza",
                    EstadoTarea = EstadoTarea.Pendiente,
                    FechaUltimoCambioEstado = null
                },
                new Tarea()
                {
                    Codigo = 2,
                    TelefonoContacto = "65456912",
                    FechaIngreso = new DateTimeOffset(2020, 5, 22, 9, 5, 0, TimeSpan.Zero),
                    DestinoCliente = "Casa Juana en Residencial Sausalito",
                    LongitudUbicacion = -89.24305m,
                    LatitudUbicacion = 13.70914m,
                    Indicaciones =
                        "Sarita, Calle Del Mirador, Residencial Sausalito, Colonia Escalón, Distrito Municipal 3, San Salvador",
                    IdCuenta = 1,
                    DetalleTarea = "Tocar la puerta en el porton verde, preguntar por Juana",
                    EstadoTarea = EstadoTarea.Pendiente,
                    FechaUltimoCambioEstado = null
                },
                new Tarea()
                {
                    Codigo = 3,
                    TelefonoContacto = "74123697",
                    FechaIngreso = new DateTimeOffset(2020, 5, 23, 12, 45, 0, TimeSpan.Zero),
                    DestinoCliente = "Tienda el Pinalito, Tecla",
                    Indicaciones =
                        "Tienda El Pinalito, Calle Jose Ciriaco López, Barrio El Centro, Santa Tecla, La Libertad, 1501, El Salvador",
                    LongitudUbicacion = -89.24305m,
                    LatitudUbicacion = 13.67447m,
                    IdCuenta = 1,
                    DetalleTarea = "Llegar a la tienda, entregar las pupusas",
                    EstadoTarea = EstadoTarea.Pendiente,
                    FechaUltimoCambioEstado = null
                });


            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes("magic789$");
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                modelBuilder.Entity<Usuario>().HasData(
                    new Usuario()
                    {
                        FechaIngreso = DateTimeOffset.Now,
                        Email = "alexanderortiz333@gmail.com",
                        Nombre = "Administrador",
                        IdCuenta = 1,
                        Activo = true,
                        Apellido = "Magic",
                        IdUsuario = "admin",
                        Password = hashBytes
                    });
            }

            

            #endregion



        }
    }
}

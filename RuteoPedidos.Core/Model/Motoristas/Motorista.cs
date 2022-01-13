using System;
using System.Collections.Generic;
using System.Text;
using RuteoPedidos.Core.Model.Tareas;

namespace RuteoPedidos.Core.Model.Motoristas
{
    /// <summary>
    /// Alguien que realiza una tarea para un destino
    /// </summary>
    public class Motorista
    {
        public Motorista()
        {
            FechaIngreso = DateTimeOffset.Now;
        }

        /// <summary>
        /// Llave
        /// </summary>
        public int Codigo { get; set; }

        /// <summary>
        /// Cuenta a la que pertenece el motorista
        /// </summary>
        public int IdCuenta { get; set; }

        /// <summary>
        /// Fecha de ingreso
        /// </summary>
        public DateTimeOffset FechaIngreso { get; set; }

        /// <summary>
        /// Dato de latitud para la ultima posicion que se tiene del motorista
        /// </summary>
        public decimal? LatitudUltimaUbicacion { get; set; }

        /// <summary>
        /// Dato de longitud para la ultima posicion que se tiene del motorista. 
        /// </summary>
        public decimal? LongitudUltimaUbicacion { get; set; }

        /// <summary>
        /// Feccha y hora de cuando se obtuvo su ubicacion
        /// </summary>
        public DateTimeOffset? FechaActualizacionUbicacion { get; set; }

        /// <summary>
        /// Nombre del motorista. Requerido
        /// </summary>
        public string NombreCompleto { get; set; }


        /// <summary>
        /// Telefono del motorolo. Requerido
        /// </summary>
        public string TelefonoContacto { get; set; }

        /// <summary>
        /// Las placas del vehiculo. Es requerido
        /// </summary>
        public string PlacasVehiculo { get; set; }

        /// <summary>
        /// Enum del tipo de vehiculo
        /// </summary>
        public TipoVehiculo TipoVehiculo { get; set; }

        /// <summary>
        /// Codigo de usuario con el que se indentificara a este motorista. Es opcional y Unique
        /// </summary>
        public string IdUsuario { get; set; }
        
        /// <summary>
        /// Texto del tipo vehiculo
        /// </summary>
        public string TextoTipoVehiculo
        {
            get => TipoVehiculo.ToString();
            set => TipoVehiculo = (TipoVehiculo)Enum.Parse(typeof(TipoVehiculo), value);
        }

        /// <summary>
        /// Indica si la entidad se encuentra activa
        /// </summary>
        public bool Activo { get; set; }

        /// <summary>
        /// Entidad cuenta
        /// </summary>
        public Cuenta Cuenta { get; set; }

        /// <summary>
        /// Entidad usuario
        /// </summary>
        public Usuario Usuario { get; set; }
    }
}

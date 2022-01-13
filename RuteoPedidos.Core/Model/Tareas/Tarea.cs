using System;
using RuteoPedidos.Core.Model.Motoristas;

namespace RuteoPedidos.Core.Model.Tareas
{
    /// <summary>
    /// Entidad que sirve como la actividad que DEBE realizarse
    /// </summary>
    public class Tarea
    {
        public Tarea()
        {
            FechaIngreso = DateTimeOffset.Now;
        }

        /// <summary>
        /// llave
        /// </summary>
        public int Codigo { get; set; }


        /// <summary>
        /// Cuenta a la que pertenece
        /// </summary>
        public int IdCuenta { get; set; }

        /// <summary>
        /// Nombre del Destino/Cliente. Es requerido
        /// </summary>
        public string DestinoCliente { get; set; }

        /// <summary>
        /// Latitud de la ubicacion
        /// </summary>
        public decimal LatitudUbicacion { get; set; }

        /// <summary>
        /// Longitud de la ubicacion
        /// </summary>
        public decimal LongitudUbicacion { get; set; }

        /// <summary>
        /// Indicaciones de como llegar. Opcionales
        /// </summary>
        public string Indicaciones { get; set; }

        /// <summary>
        /// Telefono de contacto para completar la tarea. Es opcional
        /// </summary>
        public string TelefonoContacto { get; set; }

        /// <summary>
        /// Indica lo que debe hacerse. Es requerido
        /// </summary>
        public string DetalleTarea { get; set; }

        /// <summary>
        /// Fecha de creacion de la tarea
        /// </summary>
        public DateTimeOffset FechaIngreso { get; set; }

        /// <summary>
        /// Codigo del cliente/destino usado. Es opcional
        /// </summary>
        public int? IdClienteUtilizado { get; set; }

        /// <summary>
        /// Indica el estado actual de la tarea. Debe moverse cuando se haya completado una visita
        /// </summary>
        public EstadoTarea EstadoTarea { get; set; }

        /// <summary>
        /// Texto del tipo vehiculo
        /// </summary>
        public string TextoEstadoTarea
        {
            get => EstadoTarea.ToString();
            set => EstadoTarea = (EstadoTarea)Enum.Parse(typeof(EstadoTarea), value);
        }

        /// <summary>
        /// Fecha de cuando se cambio de estado. Al inicio es null porque no se ha realizdo ningun cambio
        /// </summary>
        public DateTimeOffset? FechaUltimoCambioEstado { get; set; }

        /// <summary>
        /// Codigo del motorista que tiene asignada esta tarea. Es null porque las tareas se crean sin estar
        /// asignadas a naiden
        /// </summary>
        public int? IdMotoristaAsignado { get; set; }

        /// <summary>
        /// Fecha en la que el motorista fue asignado. Es null cuando no hay motorista asignado
        /// </summary>
        public DateTimeOffset? FechaAsignacionMotorista { get; set; }

        /// <summary>
        /// Entidad opcional
        /// </summary>
        public Destino Destino { get; set; }

        /// <summary>
        /// Cuenta de la tarea
        /// </summary>
        public Cuenta Cuenta { get; set; }

        /// <summary>
        /// Entidad motorista
        /// </summary>
        public Motorista Motorista { get; set; }
    }
}

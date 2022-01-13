using System;
using System.Collections.Generic;
using System.Text;
using RuteoPedidos.Core.Model;
using RuteoPedidos.Core.Model.Tareas;

namespace RuteoPedidos.Core.DTO.Output.Tareas
{
    /// <summary>
    /// Representa una tarea que se muestra en pantalla
    /// </summary>
    public class TareaOutputDto
    {
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
        /// Indica el estado actual de la tarea. Debe moverse cuando se haya completado una visita
        /// </summary>
        public EstadoTarea EstadoTarea { get; set; }

        /// <summary>
        /// Codigo del motorista para el que se hizo ese ordenamiento
        /// </summary>
        public int IdMotorista { get; set; }

        /// <summary>
        /// Representa el orden para una lista
        /// </summary>
        public int Orden { get; set; }

        /// <summary>
        /// En cuantos minutos llega el motorista asignado a dicho punto
        /// </summary>
        public double MinutosLlegada { get; set; }

        /// <summary>
        /// A cuantos kilometros se encuentra el motorista asignado de dicho puntp
        /// </summary>
        public double KilometrosDistancia { get; set; }

        /// <summary>
        /// Texto para el campo <see cref="MinutosLlegada"/>
        /// </summary>
        public string MinutosLlegadaTexto { get; set; }

        /// <summary>
        /// Texto para el campo <see cref="KilometrosDistancia"/>
        /// </summary>
        public string KilometrosDistanciaTexto { get; set; }

        /// <summary>
        /// Puntos ordenados a seguir para armar una ruta
        /// </summary>
        public decimal[,] PuntosRuteo { get; set; }

        public VisitaOuputDto Visita { get; set; }
    }
}

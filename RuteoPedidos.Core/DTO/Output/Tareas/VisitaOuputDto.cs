using System;
using RuteoPedidos.Core.Model;

namespace RuteoPedidos.Core.DTO.Output.Tareas
{
    /// <summary>
    /// Sirve como salida para una visita
    /// </summary>
    public class VisitaOuputDto
    {
        /// <summary>
        /// Es string porque se crea desde el app
        /// </summary>
        public string Codigo { get; set; }

        /// <summary>
        /// Codigo de la tarea relacionada. Es unique, solo pude haber un resultado por tarea
        /// </summary>
        public int IdTarea { get; set; }

        public ResultadoVisita ResultadoVisita { get; set; }

        /// <summary>
        /// Fecha en la que se genero este resultado
        /// </summary>
        public DateTimeOffset FechaIngreso { get; set; }

        /// <summary>
        /// Comentario opcional acerca de este resultado
        /// </summary>
        public string Comentario { get; set; }
    }
}

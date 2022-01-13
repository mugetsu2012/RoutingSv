using System;

namespace RuteoPedidos.Core.Model.Tareas
{
    /// <summary>
    /// Entidad que expresa la accion de haber llegado al destino
    /// </summary>
    public class Visita
    {
        public Visita()
        {
            FechaIngreso = DateTimeOffset.Now;
        }

        /// <summary>
        /// Llave. Es string porque se crea desde el app
        /// </summary>
        public string Codigo { get; set; }

        /// <summary>
        /// Codigo de la tarea relacionada. Es unique, solo pude haber un resultado por tarea
        /// </summary>
        public int IdTarea { get; set; }

        public ResultadoVisita ResultadoVisita { get; set; }

        public string TextoResultadoVisita
        {
            get => ResultadoVisita.ToString();
            set => ResultadoVisita = (ResultadoVisita)Enum.Parse(typeof(ResultadoVisita), value);
        }

        /// <summary>
        /// Fecha en la que se genero este resultado
        /// </summary>
        public DateTimeOffset FechaIngreso { get; set; }

        /// <summary>
        /// Comentario opcional acerca de este resultado
        /// </summary>
        public string Comentario { get; set; }

        /// <summary>
        /// Entidad tarea
        /// </summary>
        public Tarea Tarea { get; set; }

    }
}

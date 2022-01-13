using System;
using System.Collections.Generic;
using System.Text;
using RuteoPedidos.Core.Model;

namespace RuteoPedidos.Core.DTO.Input.Tareas
{
    public class GenerarVisitaInputDto
    {
        public GenerarVisitaInputDto()
        {
            EsApp = true;
        }

        /// <summary>
        /// Tarea para la cual se genera esta visita
        /// </summary>
        public int IdTarea { get; set; }

        /// <summary>
        /// Es opcional. Por si el APP desea tenerla local y enviarla luego cuando haya datos
        /// </summary>
        public string IdVisita { get; set; }

        /// <summary>
        /// Resultado de lo que provoco la visita
        /// </summary>
        public ResultadoVisita ResultadoVisita { get; set; }

        /// <summary>
        /// Comentario de la visita. Opcional
        /// </summary>
        public string Comentario { get; set; }

        /// <summary>
        /// Indica si es invocado desde del app
        /// </summary>
        public bool EsApp { get; set; }
    }
}

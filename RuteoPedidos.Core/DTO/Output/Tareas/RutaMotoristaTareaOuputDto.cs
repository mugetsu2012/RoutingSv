using System;
using System.Collections.Generic;
using System.Text;

namespace RuteoPedidos.Core.DTO.Output.Tareas
{
    public class RutaMotoristaTareaOuputDto: RutaEntreDosPuntosOutputDto
    {
        /// <summary>
        /// Codigo del motorista para el cual se arma esta ruta
        /// </summary>
        public int IdMotorista { get; set; }

        /// <summary>
        /// Tarea para la cual se calculo esta ruta
        /// </summary>
        public int IdTarea { get; set; }
    }
}

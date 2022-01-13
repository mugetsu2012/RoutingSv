using System;
using System.Collections.Generic;
using System.Text;
using RuteoPedidos.Core.DTO.Input.Ruteo;
using RuteoPedidos.Core.Model;

namespace RuteoPedidos.Core.DTO.Output.Tareas
{
    public class DashboardOuputDto
    {
        /// <summary>
        /// Tareas ya ordenadas del motorista
        /// </summary>
        public List<TareaOutputDto> TareasMotorista { get; set; }

        /// <summary>
        /// El tipo de orden que se solicito
        /// </summary>
        public TipoOrdenTarea TipoOrdenTarea { get; set; }

        /// <summary>
        /// Codigo del motorista para el cual se arma esta configuracion
        /// </summary>
        public int IdMotorista { get; set; }

        /// <summary>
        /// Nombre a desplegar para el motorista
        /// </summary>
        public string NombreMotorista { get; set; }

        /// <summary>
        /// Latitud actual del motorista
        /// </summary>
        public decimal LatitudMotorista { get; set; }

        /// <summary>
        /// longitud actual del motorista
        /// </summary>
        public decimal LongitudMotorista { get; set; }

    }
}

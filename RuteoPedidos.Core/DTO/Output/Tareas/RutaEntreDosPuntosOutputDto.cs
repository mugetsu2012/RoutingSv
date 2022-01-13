using System;
using System.Collections.Generic;
using System.Text;

namespace RuteoPedidos.Core.DTO.Output.Tareas
{
    /// <summary>
    /// Me regresa la ruta entre dos puntos
    /// </summary>
    public class RutaEntreDosPuntosOutputDto
    {
        /// <summary>
        /// Punto de inicio. Latitud
        /// </summary>
        public decimal LatitudInicial { get; set; }

        /// <summary>
        /// Punto de inicio. Longitud
        /// </summary>
        public decimal LongitudInicial { get; set; }

        /// <summary>
        /// Punto de fin. Latitud
        /// </summary>
        public decimal LatitudFinal { get; set; }

        /// <summary>
        /// Punto de fin longitud
        /// </summary>
        public decimal LongitudFinal { get; set; }

        /// <summary>
        /// Lista de puntos ordenados, los cuales representa la ruta para los puntos aca
        /// estipulados
        /// </summary>
        public decimal[,] PuntosRuta { get; set; }
    }
}

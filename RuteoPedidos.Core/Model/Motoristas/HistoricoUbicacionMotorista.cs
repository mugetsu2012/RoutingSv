using System;
using System.Collections.Generic;
using System.Text;

namespace RuteoPedidos.Core.Model.Motoristas
{
    /// <summary>
    /// represta una lista historica para las ubbicaciones del motorista
    /// </summary>
    public class HistoricoUbicacionMotorista
    {
        /// <summary>
        /// Llave. Es string porque podra venir del app
        /// </summary>
        public string Codigo { get; set; }

        /// <summary>
        /// Foranea del motorista
        /// </summary>
        public int IdMotorista { get; set; }

        /// <summary>
        /// Longitud de su ubicacion
        /// </summary>
        public decimal Longitud { get; set; }

        /// <summary>
        /// Latitud de su ubicacion
        /// </summary>
        public decimal Latitud { get; set; }

        /// <summary>
        /// De que fecha y hora son estos datos
        /// </summary>
        public DateTimeOffset FechaRegistroUbicacion { get; set; }

        /// <summary>
        /// Entidad motorista
        /// </summary>
        public Motorista Motorista { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using RuteoPedidos.Core.Model;

namespace RuteoPedidos.Core.DTO.Input.Motoristas
{
    public class MotoristaInputDto
    {
        /// <summary>
        /// Llave
        /// </summary>
        public int Codigo { get; set; }

        /// <summary>
        /// Cuenta a la que pertenece el motorista
        /// </summary>
        public int IdCuenta { get; set; }

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
    }
}

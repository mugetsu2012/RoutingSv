﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RuteoPedidos.WebAdmin.Models
{
    public class CrearTareaVm
    {
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
        /// Cuenta para la que se crea esta tarea
        /// </summary>
        public int IdCuenta { get; set; }

        /// <summary>
        /// Codigo del cliente/destino usado. Es opcional
        /// </summary>
        public int? IdClienteUtilizado { get; set; }

        /// <summary>
        /// Codigo del motorista. Si se envia debo asignar la tarea de una sola vez
        /// </summary>
        public int? IdMotorista { get; set; }

        /// <summary>
        /// Indica si se debe generar un cliente
        /// </summary>
        public bool GenerarCliente { get; set; }
    }
}

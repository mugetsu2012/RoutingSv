using System;
using System.Collections.Generic;
using System.Text;

namespace RuteoPedidos.Core.Model
{
    /// <summary>
    /// Entidad cuenta, para poder designar usaurios y demas a nivel de cuenta
    /// </summary>
    public class Cuenta
    {
        /// <summary>
        /// Llave primaria
        /// </summary>
        public int Codigo { get; set; }

        /// <summary>
        /// Nombre de la cuenta
        /// </summary>
        public string NombreCuenta { get; set; }
        
        /// <summary>
        /// Nombre del contacto de la persona en esta cuenta
        /// </summary>
        public string NombreContacto { get; set; }

        /// <summary>
        /// Telefono del contacto
        /// </summary>
        public string TelefonoContacto { get; set; }

        /// <summary>
        /// Indica si esta cuenta se encuentra activa
        /// </summary>
        public bool Activo { get; set; }

        public DateTimeOffset FechaIngreso { get; set; }
    }
}

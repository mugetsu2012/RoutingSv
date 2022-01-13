using System;
using System.Collections.Generic;
using System.Text;

namespace RuteoPedidos.Core.Model
{
    /// <summary>
    /// Entidad usuario que sirve para loguearse y tener los datos generales del mismo
    /// </summary>
    public class Usuario
    {
        public Usuario()
        {
            FechaIngreso = DateTimeOffset.Now;
        }

        /// <summary>
        /// Codigo del usuario
        /// </summary>
        public string IdUsuario { get; set; }

        /// <summary>
        /// Nombre del usuario
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Apellido del usuario
        /// </summary>
        public string Apellido { get; set; }

        /// <summary>
        /// Password cifrado del usuario usando md5
        /// </summary>
        public byte[] Password { get; set; }

        /// <summary>
        /// Email del usuario. Es UniqueIndex con la cuenta
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Fecha de creacion del usuario
        /// </summary>
        public DateTimeOffset FechaIngreso { get; set; }

        /// <summary>
        /// Codigo de la cuenta a la que pertenece este usuario. Unique index con el email
        /// </summary>
        public int IdCuenta { get; set; }

        /// <summary>
        /// Indica si este usuario se encuentra bloqueado
        /// </summary>
        public bool Activo { get; set; }

        /// <summary>
        /// Objeto cuenta
        /// </summary>
        public Cuenta Cuenta { get; set; }

        /// <summary>
        /// Regresa el nombre completo del usuario actual
        /// </summary>
        public string ImprimirNombreCompleto()
        {
            return Nombre + " " + Apellido;
        }
    }
}

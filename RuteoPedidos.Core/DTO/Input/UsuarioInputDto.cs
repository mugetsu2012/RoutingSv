using System;
using System.Collections.Generic;
using System.Text;

namespace RuteoPedidos.Core.DTO.Input
{
    public class UsuarioInputDto
    {
        public UsuarioInputDto()
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
        /// Codigo del motorista al cual vamos a asignar este usuario. Es opcional
        /// </summary>
        public int? IdMotorista { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RuteoPedidos.WebAdmin.Models
{
    public class CrearUsuarioVm
    {
        public CrearUsuarioVm()
        {
            EsNuevo = true;
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
        public string PasswordText { get; set; }

        /// <summary>
        /// Email del usuario. Es UniqueIndex con la cuenta
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Cuenta para la que se crea la entidad
        /// </summary>
        public string IdCuenta { get; set; }

        /// <summary>
        /// Codigo del motorista al cual asignar este usuario. Es opcional
        /// </summary>
        public int? IdMotorista { get; set; }

        public bool EsNuevo { get; set; }
    }
}

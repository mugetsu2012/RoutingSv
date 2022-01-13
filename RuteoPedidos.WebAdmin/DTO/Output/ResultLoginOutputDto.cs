using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RuteoPedidos.WebAdmin.DTO.Output
{
    public class ResultLoginOutputDto
    {
        /// <summary>
        /// el token JWT que sirve para loguearse
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Id del usaurio logueado
        /// </summary>
        public string IdUser { get; set; }

        /// <summary>
        /// Nombre del usuario logueado
        /// </summary>
        public string NombreUsuario { get; set; }

        /// <summary>
        /// Id de la cuenta del usuario logueado
        /// </summary>
        public int IdCuenta { get; set; }

        /// <summary>
        /// Fecha en la que expira el token
        /// </summary>
        public DateTimeOffset FechaExpiracion { get; set; }
    }
}

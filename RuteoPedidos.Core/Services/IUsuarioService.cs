using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RuteoPedidos.Core.DTO.Input;
using RuteoPedidos.Core.Model;

namespace RuteoPedidos.Core.Services
{
    public interface IUsuarioService
    {
        /// <summary>
        /// Permite crear un usaurio
        /// </summary>
        /// <param name="usuarioInputDto"></param>
        /// <param name="esNuevo"></param>
        /// <returns></returns>
        Task<Usuario> CrearModificarUsuarioAsync(UsuarioInputDto usuarioInputDto, bool esNuevo);

        /// <summary>
        /// Metodo que permite setear el estado del usuario, sirve para activar/desactivar
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <param name="estadoSetear"></param>
        /// <returns></returns>
        Task ActivarDesactivarUsuarioAsync(string idUsuario, bool estadoSetear);

        /// <summary>
        /// Permite validar un login
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<bool> ValidarLoginAsync(string usuario, string password);

        /// <summary>
        /// Permite extraer un usuario
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <returns></returns>
        Task<Usuario> GetUsuarioAsync(string idUsuario);


        Task<EntidadPaginada<Usuario>> GetUsuariosAsync(string nombre, int idCuenta = 0,
            FiltroEstadoUsuario estadoUsuario = FiltroEstadoUsuario.Todos, int paginaActual = 1, int itemsPerPage = 10);
    }
}

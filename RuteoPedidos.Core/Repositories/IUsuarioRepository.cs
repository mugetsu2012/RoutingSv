using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using RuteoPedidos.Core.Model;

namespace RuteoPedidos.Core.Repositories
{
    public interface IUsuarioRepository: IBaseRepository
    {
        /// <summary>
        /// Permite crear o modificar un usuario
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="esNuevo">Indica si el valor es nuevo</param>
        /// <returns></returns>
        Task<Usuario> CrearModificarUsuarioAsync(Usuario usuario, bool esNuevo);

        /// <summary>
        /// Permite sacar un usuario
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <param name="track"></param>
        /// <returns></returns>
        Task<Usuario> GetUsuarioAsync(string idUsuario, bool track = false);

        /// <summary>
        /// Permite extraer usuarios
        /// </summary>
        /// <param name="where"></param>
        /// <param name="skip"></param>
        /// <param name="top"></param>
        /// <param name="agregarIncludes"></param>
        /// <returns></returns>
        Task<List<Usuario>> GetUsuariosAsync(Expression<Func<Usuario, bool>> where, int skip, int top,
            bool agregarIncludes = false);

        /// <summary>
        /// Realiza un conteo para el where actual
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        Task<int> CountUsuariosAsync(Expression<Func<Usuario, bool>> where);
    }
}

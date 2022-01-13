using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using RuteoPedidos.Core.Model.Tareas;

namespace RuteoPedidos.Core.Repositories
{
    public interface IDestinoRepository: IBaseRepository
    {
        /// <summary>
        /// Permite agregar un destino
        /// </summary>
        /// <param name="destino"></param>
        /// <returns></returns>
        Task<Destino> AgregarDestinoAsync(Destino destino);

        /// <summary>
        /// Permite buscar destinos
        /// </summary>
        /// <param name="where"></param>
        /// <param name="skip"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<List<Destino>> BuscarDestinosAsync(Expression<Func<Destino, bool>> where, int skip, int pageSize);

        /// <summary>
        /// Me indica cuantos elementos hay para un where
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        Task<int> CountDestinoAsync(Expression<Func<Destino, bool>> where);

        /// <summary>
        /// Permite sacar un <see cref="Destino"/> por su id
        /// </summary>
        /// <param name="idDestino"></param>
        /// <param name="track"></param>
        /// <returns></returns>
        Task<Destino> GetDestinoByIdAsync(int idDestino, bool track = false);
    }
}

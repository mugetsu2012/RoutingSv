using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using RuteoPedidos.Core.Model.Motoristas;

namespace RuteoPedidos.Core.Repositories
{
    public interface IMotoristaRepository: IBaseRepository
    {
        /// <summary>
        /// Agrega o edita un motorista
        /// </summary>
        /// <param name="motorista"></param>
        /// <returns></returns>
        Task<Motorista> AgregarEditarMotoristaAsync(Motorista motorista);

        /// <summary>
        /// Permite extraer un motorista
        /// </summary>
        /// <param name="idMotorista"></param>
        /// <param name="trackear"></param>
        /// <returns></returns>
        Task<Motorista> GetMotoristaByIdAsync(int idMotorista, bool trackear = false);

        /// <summary>
        /// Obtiene una lista de motoristas
        /// </summary>
        /// <param name="where"></param>
        /// <param name="skip"></param>
        /// <param name="itemsPerPage"></param>
        /// <returns></returns>
        Task<List<Motorista>> GetMotoristasAsync(Expression<Func<Motorista, bool>> where, int skip, int itemsPerPage);

        /// <summary>
        /// Me indica para el where actual, cuantos registros hay
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        Task<int> CountMotoristasAsync(Expression<Func<Motorista, bool>> where);

        /// <summary>
        /// Permite marcar un motorista como modificado
        /// </summary>
        /// <param name="motorista"></param>
        void MarcarMotoristaModificado(Motorista motorista);
    }
}

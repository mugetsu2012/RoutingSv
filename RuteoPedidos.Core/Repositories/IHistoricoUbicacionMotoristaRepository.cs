using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RuteoPedidos.Core.Model.Motoristas;

namespace RuteoPedidos.Core.Repositories
{
    public interface IHistoricoUbicacionMotoristaRepository: IBaseRepository
    {
        /// <summary>
        /// Permite agregar un historico
        /// </summary>
        /// <param name="historicoUbicacionMotorista"></param>
        /// <returns></returns>
        Task<HistoricoUbicacionMotorista> AgregarHistoricoAsync(HistoricoUbicacionMotorista historicoUbicacionMotorista);
    }
}

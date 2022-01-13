using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RuteoPedidos.Core.Model.Tareas;

namespace RuteoPedidos.Core.Repositories
{
    public interface IHistoricoTareasOrdenamientoRepository: IBaseRepository
    {
        /// <summary>
        /// Almacena un historico almacenamiento
        /// </summary>
        /// <param name="historico"></param>
        /// <returns></returns>
        Task<HistoricoTareasOrdenamiento> GuardarHistoricoTareaAsync(HistoricoTareasOrdenamiento historico);

        /// <summary>
        /// Obtiene las listas de ordenamientos para los motoristas especificados
        /// </summary>
        /// <param name="codigosMotoristas"></param>
        /// <returns></returns>
        Task<List<HistoricoTareasOrdenamiento>> GetHistoricosTareasOrdenamientoAsync(int[] codigosMotoristas);
    }
}

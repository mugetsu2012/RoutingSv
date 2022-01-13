using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RuteoPedidos.Core.Model.Tareas;

namespace RuteoPedidos.Core.Repositories
{
    public interface IVisitaRepository: IBaseRepository
    {
        /// <summary>
        /// Verifica si existe una visita para esta tarea
        /// </summary>
        /// <param name="idTarea"></param>
        /// <returns></returns>
        Task<bool> ExisteVisitaParaTareaAsync(int idTarea);

        /// <summary>
        /// Permite agregar una visita
        /// </summary>
        /// <param name="visita"></param>
        /// <returns></returns>
        Task<Visita> AgregarVisitaAsync(Visita visita);

        /// <summary>
        /// Le doy una lista de Ids tareas y me regresa las visitas
        /// </summary>
        /// <param name="idsTareas"></param>
        /// <returns></returns>
        Task<List<Visita>> GetVisitasByTareasAsync(int[] idsTareas);
    }
}

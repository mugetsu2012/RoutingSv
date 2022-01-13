using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using RuteoPedidos.Core.DTO.Input.Tareas;
using RuteoPedidos.Core.Model.Tareas;

namespace RuteoPedidos.Core.Repositories
{
    public interface ITareaRepository: IBaseRepository
    {
        /// <summary>
        /// Marca una tarea para agregarla al context
        /// </summary>
        /// <param name="tarea"></param>
        /// <returns></returns>
        Task<Tarea> AgregarTareaAsync(Tarea tarea);

        /// <summary>
        /// Permite obtener una tarea
        /// </summary>
        /// <param name="idTarea"></param>
        /// <param name="track"></param>
        /// <returns></returns>
        Task<Tarea> GetTareaByIdAsync(int idTarea, bool track = false);

        /// <summary>
        /// Marca una tarea para eliminarla
        /// </summary>
        /// <param name="idTarea"></param>
        void MarcarEliminarTarea(int idTarea);

        /// <summary>
        /// Regresa una lista de elemetos basado en el where y la paginacion
        /// </summary>
        /// <param name="where"></param>
        /// <param name="skip"></param>
        /// <param name="pageSize"></param>
        /// <param name="agregarIncludes"></param>
        /// <returns></returns>
        Task<List<Tarea>> GetTareasAsync(Expression<Func<Tarea, bool>> where, int skip, int pageSize, bool agregarIncludes = false);

        /// <summary>
        /// Regresa cuantos elementos hay en el where
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        Task<int> CountTareasAsync(Expression<Func<Tarea, bool>> where);

    }
}

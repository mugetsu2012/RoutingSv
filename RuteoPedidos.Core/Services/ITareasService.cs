using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RuteoPedidos.Core.DTO.Input.Tareas;
using RuteoPedidos.Core.DTO.Output.Tareas;
using RuteoPedidos.Core.Model;
using RuteoPedidos.Core.Model.Motoristas;
using RuteoPedidos.Core.Model.Tareas;

namespace RuteoPedidos.Core.Services
{
    public interface ITareasService
    {
        /// <summary>
        /// Permite crear una tarea, se le puede indicar si quiero generar al cliente
        /// </summary>
        /// <param name="tareaInput"></param>
        /// <param name="generarCliente"></param>
        /// <returns></returns>
        Task<Tarea> CrearTareaAsync(TareaInputDto tareaInput, bool generarCliente = false);

        /// <summary>
        /// Busca destinos buscando por palabras clave
        /// </summary>
        /// <remarks>Busca en <see cref="Destino.Nombre"/> y en <see cref="Destino.Direccion"/></remarks>
        /// <param name="texto"></param>
        /// <param name="idCuenta">Cuenta para la que estoy buscando</param>
        /// <param name="fechaDesde"></param>
        /// <param name="fechaHasta"></param>
        /// <returns></returns>
        Task<EntidadPaginada<Destino>> BuscarDestinosAsync(string texto, int idCuenta, DateTimeOffset fechaDesde, DateTimeOffset? fechaHasta = null, int paginaActual = 1, int pageSize = 10);

        /// <summary>
        /// Toma un <see cref="Destino"/> y lo utiliza como plantilla para generar una Tarea
        /// </summary>
        /// <param name="idDestino"></param>
        /// <returns></returns>
        Task<TareaOutputDto> GenerarTareaByDestinoAsync(int idDestino);

        /// <summary>
        /// Permite eliminar una tarea. Elimina tarea en si y obviamente se la quita al motorista
        /// </summary>
        /// <param name="idTarea"></param>
        /// <remarks>Tiene una validacion que solo permite eliminar tareas pendientes</remarks>
        /// <returns></returns>
        Task EliminarTareaAsync(int idTarea);

        /// <summary>
        /// Le asigna una tarea a un motorista. Valida que esta tarea no este ya asignada
        /// </summary>
        /// <param name="idTarea"></param>
        /// <param name="idMotorista"></param>
        /// <returns></returns>
        Task AsignarTareaMotoristaAsync(int idTarea, int idMotorista);

        /// <summary>
        /// Intenta remover la entidad Tarea para el motorista en cuestion
        /// </summary>
        /// <param name="idTarea"></param>
        /// <param name="idMotorista"></param>
        /// <returns></returns>
        Task DesasignarTareaMotoristaAsync(int idTarea, int idMotorista);

        /// <summary>
        /// Ordena las tareas para un motorista. Haciendo uso del API de orden
        /// </summary>
        /// <param name="idMotorista"></param>
        /// <param name="longitudActual"></param>
        /// <param name="tipoOrden"></param>
        /// <param name="latitudActual"></param>
        /// <param name="topPendientes"></param>
        /// <returns></returns>
        Task<List<TareaOutputDto>> OrdenarTareasMotoristaAsync(int idMotorista, decimal latitudActual, decimal longitudActual , TipoOrdenTarea tipoOrden, int topPendientes = 10);

        /// <summary>
        /// Permite extraer una lista de tareas, especificando los motoristas y una fecha de inicio o fin
        /// </summary>
        /// <param name="motoristas"></param>
        /// <param name="idCuenta"></param>
        /// <param name="fechaInicio"></param>
        /// <param name="fechaFin"></param>
        /// <param name="paginaActual"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<EntidadPaginada<Tarea>> GetTareasAsync(int[] motoristas,int idCuenta, DateTimeOffset fechaInicio,
            DateTimeOffset? fechaFin = null, int paginaActual = 1, int pageSize = 10);


        /// <summary>
        /// Permite generar una visita
        /// </summary>
        /// <param name="generarVisita"></param>
        /// <returns></returns>
        Task<Visita> GenerarResultadoVisitaAsync(GenerarVisitaInputDto generarVisita);

        /// <summary>
        /// Permite armar el dashoard de la pantalla principal
        /// </summary>
        /// <param name="idCuenta"></param>
        /// <param name="tipoOrden"></param>
        /// <returns></returns>
        Task<List<DashboardOuputDto>> ArmarDashboardAsync(int idCuenta, TipoOrdenTarea tipoOrden);

        /// <summary>
        /// Permite calcular la ruta de un motorista hacia una tarea
        /// </summary>
        /// <param name="idMotorista"></param>
        /// <param name="idTarea"></param>
        /// <returns></returns>
        Task<RutaMotoristaTareaOuputDto> CalcularRutaMotoristaTareaAsync(int idMotorista, int idTarea);

    }
}

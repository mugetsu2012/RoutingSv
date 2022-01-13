using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RuteoPedidos.Core.DTO.Input.Motoristas;
using RuteoPedidos.Core.DTO.Output.Ruteo.MapBox;
using RuteoPedidos.Core.Model;
using RuteoPedidos.Core.Model.Motoristas;

namespace RuteoPedidos.Core.Services
{
    public interface IMotoristasService
    {
        /// <summary>
        /// Crea o edita los datos de un motorista.No puede setear la ubicacion actual del motorista
        /// </summary>
        /// <param name="motoristaInput"></param>
        /// <returns></returns>
        Task<Motorista> CrearEditarMotoristaAsync(MotoristaInputDto motoristaInput);

        /// <summary>
        /// Permite activar o desactivar a un motorista. Si se desactiva el motorista, debo eliminar las teras que tiene asignadas
        /// </summary>
        /// <param name="idMotorista"></param>
        /// <param name="estadoSetear"></param>
        /// <returns></returns>
        Task ActivarDesactivarMotoristaAsync(int idMotorista, bool estadoSetear);

        /// <summary>
        /// Busca un motorista por su nombre
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="idCuenta"></param>
        /// <param name="paginaActual"></param>
        /// <param name="itemsPerPage"></param>
        /// <returns></returns>
        Task<EntidadPaginada<Motorista>> BuscarMotoristasAsync(string nombre, int idCuenta, int paginaActual, int itemsPerPage);

        /// <summary>
        /// Obtiene el motorista asocioado al usuario
        /// </summary>
        /// <param name="idUser"></param>
        /// <returns></returns>
        Task<Motorista> GetMotoristaByUserAsync(string idUser);

        /// <summary>
        /// Permite actulizar la ubicacion del motorista. Genera una entrada en el historico <see cref="HistoricoUbicacionMotorista"/>
        /// </summary>
        /// <param name="actualizarUbicacionMotorista"></param>
        /// <param name="idUsuario"></param>
        /// <returns></returns>
        Task<Motorista> ActualizarUbicacionMotoristaAsync(
            ActualizarUbicacionMotoristaInputDto actualizarUbicacionMotorista, string idUsuario);

    }
}

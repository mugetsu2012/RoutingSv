using System.Threading.Tasks;
using RuteoPedidos.Core.DTO.Input.Ruteo;
using RuteoPedidos.Core.DTO.Output.Ruteo.MapBox;

namespace RuteoPedidos.Core.Providers
{
    public interface IRuteoProvider
    {
        /// <summary>
        /// Para un punto inicial y final me regresa datos de distancia y tiempo
        /// </summary>
        /// <param name="latitudInicial"></param>
        /// <param name="longitudInicial"></param>
        /// <param name="latitudFinal"></param>
        /// <param name="longitudFinal"></param>
        /// <returns></returns>
        Task<RespuestaMapBoxOutputDto> GetMapBoxDistanceAsync(decimal latitudInicial, decimal longitudInicial, decimal latitudFinal, decimal longitudFinal);

        /// <summary>
        /// Obtiene una respuesta mapbox con base en una serie de puntos ordenados
        /// </summary>
        /// <param name="puntos"></param>
        /// <returns></returns>
        Task<RespuestaMapBoxOutputDto> GetMapBoxRouteAsync(decimal[,] puntos);
    }
}

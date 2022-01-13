using System.Linq;
using System.Threading.Tasks;
using RuteoPedidos.Core.DTO.Input.Ruteo;
using RuteoPedidos.Core.DTO.Output.Ruteo.MapBox;
using RuteoPedidos.Core.Providers;
using System.Net.Http;
using Microsoft.Extensions.Caching.Memory;
using RuteoPedidos.Core.Exceptions;
using Newtonsoft.Json;
using System;

namespace RuteoPedidos.Service.Providers
{
    public class RuteoProvider: IRuteoProvider
    {
        private readonly IMemoryCache _cache;
        private readonly IHttpClientFactory _clientFactory;
        private readonly MapBoxConfigInputDto _configObject;

        public RuteoProvider(IHttpClientFactory clientFactory, MapBoxConfigInputDto configObject, IMemoryCache cache)
        {
            _clientFactory = clientFactory;
            _configObject = configObject;
            _cache = cache;
        }

        public async Task<RespuestaMapBoxOutputDto> GetMapBoxDistanceAsync(decimal latitudInicial, decimal longitudInicial, decimal latitudFinal, decimal longitudFinal)
        {
            //Procedemos a realizar pruebas, preparamos la URL template
            string urlGet = GetUrlPeticion(latitudInicial, longitudInicial, latitudFinal, longitudFinal, _configObject);

            RespuestaMapBoxOutputDto respuestaMapBox;

            // Look for cache key.
            if (!_cache.TryGetValue(urlGet, out respuestaMapBox))
            {
                // Key not in cache, so get data.
                respuestaMapBox = await ObtenerRespuestaMapbox(urlGet);

                // Set cache options.
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    // Keep in cache for this time, reset time if accessed.
                    .SetSlidingExpiration(TimeSpan.FromMinutes(3));

                // Save data in cache.
                _cache.Set(urlGet, respuestaMapBox, cacheEntryOptions);
            }

            return respuestaMapBox;
        }

        public async Task<RespuestaMapBoxOutputDto> GetMapBoxRouteAsync(decimal[,] puntos)
        {
            //Procedemos a realizar pruebas, preparamos la URL template
            string urlGet = GetUrlPeticion(puntos, _configObject);

            RespuestaMapBoxOutputDto respuestaMapBox = await ObtenerRespuestaMapbox(urlGet);

            return respuestaMapBox;

        }


        private string GetUrlPeticion(decimal latitudInicial, decimal longitudInicial, decimal latitudFinal, decimal longitudFinal, MapBoxConfigInputDto configObject)
        {
            string urlGet =
                $"{configObject.UrlBase}/{configObject.RoutingProfile}/{longitudInicial},{latitudInicial};{longitudFinal},{latitudFinal}" +
                $"?alternatives={configObject.UseAlternatives.ToString().ToLower()}" +
                $"&geometries={configObject.Geometries}" +
                $"&steps={configObject.UseSteps.ToString().ToLower()}" +
                $"&access_token={configObject.AccessToken}";

            return urlGet;
        }

        private string GetUrlPeticion(decimal [,] puntos, MapBoxConfigInputDto configObject)
        {
            string coordenadasString = string.Empty;
            int lenghtArr = puntos.GetLength(0);

            for (int i = 0; i < lenghtArr; i++)
            {

                coordenadasString += puntos[i, 1] + "," + puntos[i, 0];
                if (i != lenghtArr -1)
                {
                    coordenadasString += ";";
                }
            }


            string urlGet =
                $"{configObject.UrlBase}/{configObject.RoutingProfile}/{coordenadasString}" +
                $"?alternatives={configObject.UseAlternatives.ToString().ToLower()}" +
                $"&geometries={configObject.Geometries}" +
                $"&steps={configObject.UseSteps.ToString().ToLower()}" +
                $"&access_token={configObject.AccessToken}";

            return urlGet;
        }

        private async Task<RespuestaMapBoxOutputDto> ObtenerRespuestaMapbox(string urlGet)
        {
            //Ahora hacemos la peticion GET
            HttpClient client = _clientFactory.CreateClient();

            HttpResponseMessage responseMessage = await client.GetAsync(urlGet);

            string content = await responseMessage.Content.ReadAsStringAsync();

            if (responseMessage.IsSuccessStatusCode == false)
            {
                throw new MapBoxException(
                    $"Ocurrio un error al intentar llamar a MapBox. StatusCode : {responseMessage.StatusCode}, Message: {content}");
            }


            //En este punto, la peticion salio bien, procedemos a deserializar el objeto
            RespuestaMapBoxOutputDto respuestaMapBox = JsonConvert.DeserializeObject<RespuestaMapBoxOutputDto>(content);

            return respuestaMapBox;
        }
    }
}

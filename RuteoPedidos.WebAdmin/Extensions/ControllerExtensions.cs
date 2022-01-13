using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RuteoPedidos.Core.Exceptions;
using RuteoPedidos.WebAdmin.Models;

namespace RuteoPedidos.WebAdmin.Extensions
{
    public static class ControllerExtensions
    {
        public static IActionResult GenerarBadRequestError<T>(this ControllerBase controller, ILogger<T> logger, RuteoException exception)
        {
            logger.LogError(exception, "Ocurrio un error al generar la vsita");
            ObjetoErrorApi objetoErrorApi = new ObjetoErrorApi(exception.TipoError, exception.Message);
            return new BadRequestObjectResult(objetoErrorApi);
        }

        public static void Put<T>(this ITempDataDictionary tempData, string key, T value) where T : class
        {
            tempData[key] = JsonConvert.SerializeObject(value);
        }

        public static T Get<T>(this ITempDataDictionary tempData, string key) where T : class
        {
            object o;
            tempData.TryGetValue(key, out o);
            return o == null ? null : JsonConvert.DeserializeObject<T>((string)o);
        }
    }
}

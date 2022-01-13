using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RuteoPedidos.Core.Model;

namespace RuteoPedidos.WebAdmin.Models
{
    public class ObjetoErrorApi
    {
        public ObjetoErrorApi(TipoError tipoError, string mensaje)
        {
            TipoError = tipoError;
            Mensaje = mensaje;
        }

        public TipoError TipoError { get; set; }

        public string Mensaje { get; set; }
    }
}

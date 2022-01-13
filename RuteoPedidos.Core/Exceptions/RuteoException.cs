using System;
using System.Collections.Generic;
using System.Text;
using RuteoPedidos.Core.Model;

namespace RuteoPedidos.Core.Exceptions
{
    public class RuteoException: Exception
    {
        public RuteoException(TipoError tipoError, string message) : base(message)
        {
            TipoError = tipoError;
        }

        public TipoError TipoError { get; set; }
    }
}

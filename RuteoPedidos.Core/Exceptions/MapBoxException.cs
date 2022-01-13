using System;
using System.Collections.Generic;
using System.Text;

namespace RuteoPedidos.Core.Exceptions
{
    public class MapBoxException: Exception
    {
        public MapBoxException(string message): base(message)
        {
            
        }
    }
}

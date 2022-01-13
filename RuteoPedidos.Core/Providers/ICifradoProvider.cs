using System;
using System.Collections.Generic;
using System.Text;

namespace RuteoPedidos.Core.Providers
{
    public interface ICifradoProvider
    {
        /// <summary>
        /// Toma el text y lo hashea a MD5
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        byte[] HashToMd5(string text);
    }
}

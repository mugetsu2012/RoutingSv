using System;
using System.Collections.Generic;
using System.Text;
using RuteoPedidos.Core.Providers;

namespace RuteoPedidos.Service.Providers
{
    public class CifradoProvider: ICifradoProvider
    {
        public byte[] HashToMd5(string text)
        {
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(text);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                return hashBytes;
            }
        }
    }
}

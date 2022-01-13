using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace RuteoPedidos.WebAdmin.Models
{
    public class ObjetoModelStateView<T>
    {
        public T Modelo { get; set; }

        public List<KeyValuePair<string, string>> ElementosError { get; set; }
    }
}

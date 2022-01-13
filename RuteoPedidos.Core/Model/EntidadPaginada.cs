using System;
using System.Collections.Generic;
using System.Text;

namespace RuteoPedidos.Core.Model
{
    public class EntidadPaginada<T>
    {
        public List<T> Resultados { get; set; }

        /// <summary>
        /// La cantidad total de items que existen
        /// </summary>
        public int TotalItems { get; set; }

        public int PaginaActual { get; set; }

        public int PageSize { get; set; }

        public int TotalPaginas => (int)Math.Ceiling((decimal)TotalItems / (decimal)PageSize);
    }
}

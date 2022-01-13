using System;
using System.Collections.Generic;
using System.Text;

namespace RuteoPedidos.Core.DTO.Output.Ruteo.MapBox
{
    public class Geometry
    {
        public List<List<double>> Coordinates { get; set; }

        public string Type { get; set; }
    }
}

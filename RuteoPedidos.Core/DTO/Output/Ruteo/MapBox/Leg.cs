using System;
using System.Collections.Generic;
using System.Text;

namespace RuteoPedidos.Core.DTO.Output.Ruteo.MapBox
{
    public class Leg
    {
        public string Summary { get; set; }
        public double Weight { get; set; }
        public double Duration { get; set; }
        public List<object> Steps { get; set; }
        public double Distance { get; set; }
    }
}

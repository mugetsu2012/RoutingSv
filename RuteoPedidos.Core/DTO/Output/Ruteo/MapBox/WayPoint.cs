using System;
using System.Collections.Generic;
using System.Text;

namespace RuteoPedidos.Core.DTO.Output.Ruteo.MapBox
{
    public class Waypoint
    {
        public double Distance { get; set; }
        public string Name { get; set; }
        public List<double> Location { get; set; }
    }
}

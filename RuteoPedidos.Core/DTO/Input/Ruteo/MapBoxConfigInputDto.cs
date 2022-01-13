using System;
using System.Collections.Generic;
using System.Text;

namespace RuteoPedidos.Core.DTO.Input.Ruteo
{
    public class MapBoxConfigInputDto
    {
        public string UrlBase { get; set; }

        public string AccessToken { get; set; }

        public string RoutingProfile { get; set; }

        public bool UseAlternatives { get; set; }

        public bool UseSteps { get; set; }

        public string Geometries { get; set; }
    }
}

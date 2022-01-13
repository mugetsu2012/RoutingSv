using System;
using System.Collections.Generic;
using System.Text;

namespace RuteoPedidos.Core.DTO.Input.Motoristas
{
    /// <summary>
    /// Sirve para actualizar los datos de ubicacion del motorista 
    /// </summary>
    public class ActualizarUbicacionMotoristaInputDto
    {
        public decimal Latitud { get; set; }

        public decimal Longitud { get; set; }
    }
}

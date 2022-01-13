using System;
using System.Collections.Generic;
using System.Text;
using RuteoPedidos.Core.Model.Motoristas;

namespace RuteoPedidos.Core.Model.Tareas
{
    public class HistoricoTareasOrdenamiento
    {
        public HistoricoTareasOrdenamiento()
        {
            FechaIngreso = DateTimeOffset.Now;
        }

        public int Codigo { get; set; }

        /// <summary>
        /// Codigo del motorista para el cual se realizo el ordenamiento
        /// </summary>
        public int IdMotorista { get; set; }

        /// <summary>
        /// Lista de tareas involucradas al realizar este ordenamiento
        /// </summary>
        public int[] CodigoTareasInvolucradas { get; set; }

        /// <summary>
        /// Fecha en la que se realizo el ordenamiento
        /// </summary>
        public DateTimeOffset FechaIngreso { get; set; }

        public Motorista Motorista { get; set; }
    }
}

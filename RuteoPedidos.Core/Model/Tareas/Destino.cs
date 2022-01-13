using System;

namespace RuteoPedidos.Core.Model.Tareas
{
    /// <summary>
    /// Representa un destino de entrega
    /// </summary>
    public class Destino
    {
        public Destino()
        {
            FechaIngreso = DateTimeOffset.Now;
        }

        /// <summary>
        /// Llave primaria
        /// </summary>
        public int Codigo { get; set; }

        /// <summary>
        /// Id de la cuenta a la que pertenece este destino
        /// </summary>
        public int IdCuenta { get; set; }

        /// <summary>
        /// Nombre del destino. Puede ser el nombre del cliente o una direccion
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Fecha de ingreso
        /// </summary>
        public DateTimeOffset FechaIngreso { get; set; }

        /// <summary>
        /// Dato de latitud. Tiene precision 9,6
        /// </summary>
        public decimal LatitudUbicacion { get; set; }

        /// <summary>
        /// Dato de longitud ubicacion. Tiene precision 9,6
        /// </summary>
        public decimal LongitudUbicacion { get; set; }

        /// <summary>
        /// Direccion o indicaciones. Es opcional y es mas de caracter informativo
        /// </summary>
        public string Direccion { get; set; }

        /// <summary>
        /// Dato informativo para contactar al destino. Es opcional
        /// </summary>
        public string TelefonoContacto { get; set; }

        /// <summary>
        /// Indica si este destino se encurntra activo
        /// </summary>
        public bool Activo { get; set; }

        /// <summary>
        /// Obj cuenta
        /// </summary>
        public Cuenta Cuenta { get; set; }
    }
}

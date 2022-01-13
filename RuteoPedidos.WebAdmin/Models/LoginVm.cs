using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RuteoPedidos.WebAdmin.Models
{
    public class LoginVm
    {
        [Required(ErrorMessage = "El nombre de usuario es requerido")]
        public string Usuario { get; set; }

        [Required(ErrorMessage = "La constraseña es requerida")]
        public string Password { get; set; }
    }
}

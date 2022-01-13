using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RuteoPedidos.Core.Model;
using RuteoPedidos.Core.Services;

namespace RuteoPedidos.WebAdmin.Controllers
{
    [Authorize(AuthenticationSchemes = "Cookies")]
    public class UsersController: Controller
    {
        private readonly IUsuarioService _usuarioService;

        public UsersController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        public async Task<IActionResult> Index()
        {
            int idCuenta = await GetIdCuentaAsync();
            EntidadPaginada<Usuario> entidadPaginada =
                await _usuarioService.GetUsuariosAsync(string.Empty, idCuenta, FiltroEstadoUsuario.Todos, 1,
                    int.MaxValue);

            return View(entidadPaginada.Resultados);
        }

        private async Task<int> GetIdCuentaAsync()
        {
            int idCuenta;

            //Saco la cuenta claim del user
            string cuentaClaim = User.Claims.FirstOrDefault(x => x.Type == "idCuenta")?.Value;

            //Si no hay claims, saco la cuenta de la base
            if (string.IsNullOrEmpty(cuentaClaim))
            {
                Usuario usuario = await _usuarioService.GetUsuarioAsync(User.Identity.Name);
                idCuenta = usuario.IdCuenta;
            }
            else
            {
                //Si hay algo en el claim, parseo
                idCuenta = int.Parse(cuentaClaim);
            }

            return idCuenta;
        }
    }
}

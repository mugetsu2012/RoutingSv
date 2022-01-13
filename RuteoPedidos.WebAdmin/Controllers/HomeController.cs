using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RuteoPedidos.Core.DTO.Input.Tareas;
using RuteoPedidos.Core.DTO.Output.Tareas;
using RuteoPedidos.Core.Model;
using RuteoPedidos.Core.Model.Motoristas;
using RuteoPedidos.Core.Model.Tareas;
using RuteoPedidos.Core.Repositories;
using RuteoPedidos.Core.Services;
using RuteoPedidos.WebAdmin.Models;

namespace RuteoPedidos.WebAdmin.Controllers
{
    [Authorize(AuthenticationSchemes = "Cookies")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMotoristasService _motoristasService;
        private readonly ITareasService _tareasService;
        private readonly IUsuarioService _usuarioService;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger,
            IMotoristasService motoristasService,
            ITareasService tareasService, 
            IUsuarioService usuarioService, 
            IConfiguration configuration)
        {
            _logger = logger;
            _motoristasService = motoristasService;
            _tareasService = tareasService;
            _usuarioService = usuarioService;
            _configuration = configuration;
        }

        
        public async Task<IActionResult> Index()
        {
            int idCuenta = await GetIdCuentaAsync();

            List<DashboardOuputDto> dashBoard =
                await _tareasService.ArmarDashboardAsync(idCuenta, TipoOrdenTarea.MasCercana);

            _logger.LogWarning("Estoy por regresar a la view el siguiente objeto: " + JsonConvert.SerializeObject(dashBoard));
            return View(dashBoard);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #region Metodos Privados

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

        #endregion
    }
}

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using RuteoPedidos.Core.DTO.Input.Tareas;
using RuteoPedidos.Core.Model;
using RuteoPedidos.Core.Model.Tareas;
using RuteoPedidos.Core.Services;
using RuteoPedidos.WebAdmin.Models;

namespace RuteoPedidos.WebAdmin.Controllers
{
    [Authorize(AuthenticationSchemes = "Cookies")]
    public class TareasController: Controller
    {
        private readonly IMapper _mapper;
        private readonly ITareasService _tareasService;
        private readonly IUsuarioService _usuarioService;

        public TareasController(
            ITareasService tareasService, 
            IMapper mapper,
            IUsuarioService usuarioService)
        {
            _tareasService = tareasService;
            _mapper = mapper;
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {

            int idCuenta = await GetIdCuentaAsync();
            //Listar las tareas
            EntidadPaginada<Tarea> tareas = await _tareasService.GetTareasAsync(new int[] { }, idCuenta,
                DateTimeOffset.Now.AddYears(-7), null, 1,
                int.MaxValue);

            return View(tareas.Resultados);
        }

        [HttpPost]
        public async Task<IActionResult> GuardarTarea(CrearTareaVm crearTareaVm)
        {
            TareaInputDto tareaInput = _mapper.Map<CrearTareaVm, TareaInputDto>(crearTareaVm);

            await _tareasService.CrearTareaAsync(tareaInput, crearTareaVm.GenerarCliente);

            return new JsonResult(new {exito = true});
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

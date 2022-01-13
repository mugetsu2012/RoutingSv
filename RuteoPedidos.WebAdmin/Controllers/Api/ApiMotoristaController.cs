using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RuteoPedidos.Core.DTO.Input.Motoristas;
using RuteoPedidos.Core.DTO.Output.Tareas;
using RuteoPedidos.Core.Model;
using RuteoPedidos.Core.Model.Motoristas;
using RuteoPedidos.Core.Services;

namespace RuteoPedidos.WebAdmin.Controllers.Api
{
    [ApiController]
    [Authorize]
    [Route("api/motorista")]
    public class ApiMotoristaController: ControllerBase
    {
        private readonly ITareasService _tareasService;
        private readonly IMotoristasService _motoristasService;

        public ApiMotoristaController(ITareasService tareasService, IMotoristasService motoristasService)
        {
            _tareasService = tareasService;
            _motoristasService = motoristasService;
        }

        [HttpGet("tareas")]
        [ProducesResponseType(typeof(List<TareaOutputDto>), 200)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> GetListadoTareas(decimal latitudActual, decimal longitudActual, TipoOrdenTarea tipoOrden, int topTareasFinalizadas = 10)
        {
            //Sacar el usuario logueado
            Motorista motorista = await _motoristasService.GetMotoristaByUserAsync(User.Identity.Name);

            //Comparamos que si el motorista es null, reventar con 403
            if (motorista == null || motorista.Activo == false)
            {
                return new ForbidResult();
            }

            //Como el motorista no es null, procedo a regresar la list de tareas asignadas al motorista

            //Voy a sacar las tareas del motorista
            List<TareaOutputDto> tareas = await _tareasService.OrdenarTareasMotoristaAsync(motorista.Codigo, latitudActual, longitudActual,
                tipoOrden, topTareasFinalizadas);

            return Ok(tareas);

        }

        [HttpPost("ubicacion")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> ActualizarUbicacion(ActualizarUbicacionMotoristaInputDto actualizarUbicacionMotoristaDto)
        {
            await _motoristasService.ActualizarUbicacionMotoristaAsync(actualizarUbicacionMotoristaDto,
                User.Identity.Name);

            return Ok();
        }
    }
}

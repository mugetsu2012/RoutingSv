using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using RuteoPedidos.Core.DTO.Input.Tareas;
using RuteoPedidos.Core.DTO.Output.Tareas;
using RuteoPedidos.Core.Exceptions;
using RuteoPedidos.Core.Model;
using RuteoPedidos.Core.Model.Motoristas;
using RuteoPedidos.Core.Model.Tareas;
using RuteoPedidos.Core.Services;
using RuteoPedidos.WebAdmin.Extensions;
using RuteoPedidos.WebAdmin.Models;
using Serilog;

namespace RuteoPedidos.WebAdmin.Controllers.Api
{
    [ApiController]
    [Authorize]
    [Route("api/tareas")]
    public class ApiTareasController: ControllerBase
    {
        private readonly ILogger<ApiTareasController> _logger;
        private readonly ITareasService _tareasService;
        private readonly IMapper _mapper;

        public ApiTareasController(
            ILogger<ApiTareasController> logger,
            ITareasService tareasService,
            IMapper mapper)
        {
            _logger = logger;
            _tareasService = tareasService;
            _mapper = mapper;
        }

        [HttpPost("visita")]
        [ProducesResponseType(typeof(Visita), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GenerarVisita(GenerarVisitaInputDto visitaDto)
        {
            try
            {
                Visita visita = await _tareasService.GenerarResultadoVisitaAsync(visitaDto);
                return Ok(visita);
            }
            catch (RuteoException e)
            {
                //Si el estado de la tarea no es valido, pero me llaman desde el app, ignoro el error
                if (e.TipoError == TipoError.EstadoInvalidoVisita && visitaDto.EsApp)
                {
                    return Ok(new Visita());
                }

                return this.GenerarBadRequestError(_logger, e);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(TareaOutputDto), 200)]
        public async Task<IActionResult> CrearTarea(TareaInputDto tareaInputDto, bool generarCliente = false)
        {
            Tarea tarea = await _tareasService.CrearTareaAsync(tareaInputDto, generarCliente);

            //Mapeo al tarea output
            TareaOutputDto tareaOutput = _mapper.Map<Tarea, TareaOutputDto>(tarea);

            return Ok(tareaOutput);
        }
        
    }
}

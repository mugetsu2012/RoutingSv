using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RuteoPedidos.Core.Model;
using RuteoPedidos.Core.Services;
using RuteoPedidos.WebAdmin.ApplicationService;
using RuteoPedidos.WebAdmin.DTO.Output;

namespace RuteoPedidos.WebAdmin.Controllers.Api
{
    [ApiController]
    [Authorize]
    [Route("api/cuenta")]
    public class ApiCuentaController: ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public ApiCuentaController(IUsuarioService usuarioService,
            IUserService userService,
            IConfiguration configuration)
        {
            _usuarioService = usuarioService;
            _userService = userService;
            _configuration = configuration;
        }


        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ResultLoginOutputDto), 200)]
        public async Task<IActionResult> Login(string user, string password)
        {
            //Primero validamos el login
            bool loginValido = await _usuarioService.ValidarLoginAsync(user, password);

            //Si fallo el login, le digo que no esta autorizado
            if (loginValido == false)
            {
                return Unauthorized();
            }

            //Saco el usuario
            Usuario usuario = await _usuarioService.GetUsuarioAsync(user);

            //Calculo la fecha de expiracion
            DateTime fechaExpiracion = DateTime.Now.AddDays(double.Parse(_configuration["DiasVencimientoToken"]));

            string token = _userService.GenerarToken(user, usuario.ImprimirNombreCompleto(), usuario.IdCuenta,
                fechaExpiracion); 

            //En este punto, puedo regresarle los datos correctos
            ResultLoginOutputDto resultLoginOutputDto = new ResultLoginOutputDto()
            {
                IdCuenta = usuario.IdCuenta,
                FechaExpiracion = fechaExpiracion,
                IdUser = user,
                NombreUsuario = usuario.ImprimirNombreCompleto(),
                Token = token
            };

            return Ok(resultLoginOutputDto);
        }

        [HttpGet("privado-test")]
        public IActionResult PrivadoTest()
        {
            string[] arrsStrings = new[] {"Valor 1", "Valor 2", "Valor 3"};
            return Ok(arrsStrings);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using RuteoPedidos.Core.DTO.Input;
using RuteoPedidos.Core.Model;
using RuteoPedidos.Core.Providers;
using RuteoPedidos.Core.Services;
using RuteoPedidos.WebAdmin.Extensions;
using RuteoPedidos.WebAdmin.Models;

namespace RuteoPedidos.WebAdmin.Controllers
{
    public class CuentaController : Controller
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IMapper _mapper;
        private readonly ICifradoProvider _cifradoProvider;

        public CuentaController(IUsuarioService usuarioService,
            IMapper mapper, 
            ICifradoProvider cifradoProvider)
        {
            _usuarioService = usuarioService;
            _mapper = mapper;
            _cifradoProvider = cifradoProvider;
        }

        [HttpGet]
        public IActionResult CrearUsuario()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GuardarUsuario(CrearUsuarioVm crearUsuarioVm)
        {
            UsuarioInputDto usuarioInput = _mapper.Map<CrearUsuarioVm, UsuarioInputDto>(crearUsuarioVm);
            usuarioInput.Activo = true;
            usuarioInput.Password = _cifradoProvider.HashToMd5(crearUsuarioVm.PasswordText);

            await _usuarioService.CrearModificarUsuarioAsync(usuarioInput, true);
            return RedirectToAction("CrearUsuario");
        }


        [HttpPost]
        public async Task<IActionResult> CrearModificarUsuario(CrearUsuarioVm crearUsuarioVm)
        {
            UsuarioInputDto usuarioInput = _mapper.Map<CrearUsuarioVm, UsuarioInputDto>(crearUsuarioVm);
            usuarioInput.Activo = true;
            usuarioInput.Password = _cifradoProvider.HashToMd5(crearUsuarioVm.PasswordText);

            await _usuarioService.CrearModificarUsuarioAsync(usuarioInput, crearUsuarioVm.EsNuevo);
            return Json(new {exito = true});
        }

        [HttpGet]
        public IActionResult Login()
        {
            ObjetoModelStateView<LoginVm> objetoModelStateView = TempData.Get<ObjetoModelStateView<LoginVm>>("modelLogin");

            //Logica para leer el model state
            if (objetoModelStateView != null)
            {
                foreach (KeyValuePair<string, string> keyValuePair in objetoModelStateView.ElementosError)
                {
                    ModelState.AddModelError(keyValuePair.Key, keyValuePair.Value);
                }
            }

            return View(objetoModelStateView?.Modelo);
        }

        [HttpPost]
        public async Task<IActionResult> Ingresar(LoginVm loginVm)
        {
            //Primero validamos que la data vaya bien
            if (ModelState.IsValid == false)
            {
                ObjetoModelStateView<LoginVm> objetoModelStateView = new ObjetoModelStateView<LoginVm>()
                {
                    ElementosError = ModelState.Where(y => y.Value.Errors.Any()).Select(x =>
                        new KeyValuePair<string, string>(x.Key, x.Value.Errors.First().ErrorMessage)).ToList(),
                    Modelo = loginVm
                };
                TempData.Put("modelLogin", objetoModelStateView);
                return RedirectToAction("Login");
            }

            bool loginValido = await _usuarioService.ValidarLoginAsync(loginVm.Usuario, loginVm.Password);

            if (loginValido == false)
            {
                ModelState.AddModelError(string.Empty, "Los datos de ingreso no son válidos");
                ObjetoModelStateView<LoginVm> objetoModelStateView = new ObjetoModelStateView<LoginVm>()
                {
                    ElementosError = ModelState.Where(y => y.Value.Errors.Any()).Select(x =>
                        new KeyValuePair<string, string>(x.Key, x.Value.Errors.First().ErrorMessage)).ToList(),
                    Modelo = loginVm
                };
                TempData.Put("modelLogin", objetoModelStateView);
                return RedirectToAction("Login");
            }

            //Como es login valido, mando a sacar el usuario para tener datos
            Usuario usuario = await _usuarioService.GetUsuarioAsync(loginVm.Usuario);

            //En este punto el login es valido y lo mandamos al home
            ClaimsIdentity identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, loginVm.Usuario));
            identity.AddClaim(new Claim(ClaimTypes.Name, loginVm.Usuario));
            identity.AddClaim(new Claim(ClaimTypes.Email, usuario.Email));
            //Agregamos claims nuestros
            identity.AddClaim(new Claim("nombreCompleto", usuario.ImprimirNombreCompleto()));
            identity.AddClaim(new Claim("idCuenta", usuario.IdCuenta.ToString()));

            ClaimsPrincipal principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties { IsPersistent = true });

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync("Cookies");
            return RedirectToAction("Login");
        }
    }
}
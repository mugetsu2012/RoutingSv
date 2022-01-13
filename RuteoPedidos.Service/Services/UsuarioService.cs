using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using RuteoPedidos.Core.DTO.Input;
using RuteoPedidos.Core.Exceptions;
using RuteoPedidos.Core.Model;
using RuteoPedidos.Core.Model.Motoristas;
using RuteoPedidos.Core.Providers;
using RuteoPedidos.Core.Repositories;
using RuteoPedidos.Core.Services;

namespace RuteoPedidos.Service.Services
{
    public class UsuarioService: IUsuarioService
    {
        private readonly IMapper _mapper;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ICifradoProvider _cifradoProvider;
        private readonly IMotoristaRepository _motoristaRepository;

        public UsuarioService( IMapper mapper,
            IUsuarioRepository usuarioRepository,
            ICifradoProvider cifradoProvider,
            IMotoristaRepository motoristaRepository)
        {
            _usuarioRepository = usuarioRepository;
            _cifradoProvider = cifradoProvider;
            _motoristaRepository = motoristaRepository;
            _mapper = mapper;
        }

        public async Task<Usuario> CrearModificarUsuarioAsync(UsuarioInputDto usuarioInputDto, bool esNuevo)
        {
            if (esNuevo)
            {
                Usuario usuario = _mapper.Map<UsuarioInputDto, Usuario>(usuarioInputDto);
                await _usuarioRepository.CrearModificarUsuarioAsync(usuario, true);

                //Si me dijo que lo asigne a un motorista de un solo, saco al motorongo
                if (usuarioInputDto.IdMotorista != null)
                {
                    Motorista motorista =
                        await _motoristaRepository.GetMotoristaByIdAsync(usuarioInputDto.IdMotorista.Value, true);

                    //Si el motorista ya tiene usuario asignado, debo reventar
                    if (motorista.IdUsuario != null)
                    {
                        throw new RuteoException(TipoError.MotoristaConUsusario, $"El motorista {motorista.Codigo} ya tiene un usuario asignado");
                    }

                    //Llegado a este punto, es seguro asignarle el usuario al motorista 
                    motorista.IdUsuario = usuarioInputDto.IdUsuario;
                }


                await _usuarioRepository.SaveChangesAsync();
                return usuario;
            }

            //Si no es nuevo, lo saco de la base
            Usuario usuarioExistente = await _usuarioRepository.GetUsuarioAsync(usuarioInputDto.IdUsuario, true);
            usuarioExistente.Nombre = usuarioExistente.Nombre;
            usuarioExistente.Apellido = usuarioExistente.Apellido;
            usuarioExistente.Email = usuarioExistente.Email;

            await _usuarioRepository.SaveChangesAsync();
            return usuarioExistente;
        }

        public async Task ActivarDesactivarUsuarioAsync(string idUsuario, bool estadoSetear)
        {
            //Saco al usaurio
            Usuario usuario = await _usuarioRepository.GetUsuarioAsync(idUsuario, true);

            usuario.Activo = estadoSetear;

            await _usuarioRepository.SaveChangesAsync();
        }

        public async Task<bool> ValidarLoginAsync(string usuario, string password)
        {
            //Primero saco el usuario
            Usuario usuarioTentativo = await _usuarioRepository.GetUsuarioAsync(usuario);

            //Si no existe el usuario o esta inactivo, entonces salu
            if (usuarioTentativo == null || usuarioTentativo.Activo == false)
            {
                return false;
            }

            //Mando a hashear el password para verficiar su validez
            byte[] hashTentativo = _cifradoProvider.HashToMd5(password);

            return usuarioTentativo.Password.SequenceEqual(hashTentativo);
        }

        public async Task<Usuario> GetUsuarioAsync(string idUsuario)
        {
            return await _usuarioRepository.GetUsuarioAsync(idUsuario);
        }

        public async Task<EntidadPaginada<Usuario>> GetUsuariosAsync(string nombre, int idCuenta = 0, FiltroEstadoUsuario estadoUsuario = FiltroEstadoUsuario.Todos,
            int paginaActual = 1, int itemsPerPage = 10)
        {
            int skip = (paginaActual - 1) * itemsPerPage;
            bool esNombreVacio = string.IsNullOrEmpty(nombre);

            Expression<Func<Usuario, bool>> where = usuario =>
                (esNombreVacio || usuario.Nombre.Contains(nombre) || usuario.Apellido.Contains(nombre)) &&
                (idCuenta == 0 || usuario.IdCuenta == idCuenta) &&
                ((estadoUsuario == FiltroEstadoUsuario.Todos) ||
                 (estadoUsuario == FiltroEstadoUsuario.Activo && usuario.Activo) ||
                 (estadoUsuario == FiltroEstadoUsuario.Inactivo && usuario.Activo == false));

            List<Usuario> usuarios = await _usuarioRepository.GetUsuariosAsync(where, skip, itemsPerPage, true);
            int totalItems = await _usuarioRepository.CountUsuariosAsync(where);

            EntidadPaginada<Usuario> entoEntidadPaginada = new EntidadPaginada<Usuario>()
            {
                PageSize = itemsPerPage,
                TotalItems = totalItems,
                PaginaActual = paginaActual,
                Resultados = usuarios
            };

            return entoEntidadPaginada;
        }
    }
}

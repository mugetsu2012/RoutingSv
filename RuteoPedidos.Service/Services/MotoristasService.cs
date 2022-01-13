using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using RuteoPedidos.Core.DTO.Input.Motoristas;
using RuteoPedidos.Core.Exceptions;
using RuteoPedidos.Core.Model;
using RuteoPedidos.Core.Model.Motoristas;
using RuteoPedidos.Core.Repositories;
using RuteoPedidos.Core.Services;

namespace RuteoPedidos.Service.Services
{
    public class MotoristasService: IMotoristasService
    {
        private readonly IMapper _mapper;
        private readonly IMotoristaRepository _motoristaRepository;
        private readonly IHistoricoUbicacionMotoristaRepository _historicoUbicacionMotorista;

        public MotoristasService(IMotoristaRepository motoristaRepository, 
            IMapper mapper, 
            IHistoricoUbicacionMotoristaRepository historicoUbicacionMotorista)
        {
            _motoristaRepository = motoristaRepository;
            _mapper = mapper;
            _historicoUbicacionMotorista = historicoUbicacionMotorista;
        }

        public async Task<Motorista> CrearEditarMotoristaAsync(MotoristaInputDto motoristaInput)
        {
            if (motoristaInput.Codigo == 0)
            {
                Motorista motoristaNuevo = _mapper.Map<MotoristaInputDto, Motorista>(motoristaInput);
                await _motoristaRepository.AgregarEditarMotoristaAsync(motoristaNuevo);
                await _motoristaRepository.SaveChangesAsync();
                return motoristaNuevo;
            }


            //Como es editar, debo sacar la data de la base
            Motorista motorista = await _motoristaRepository.GetMotoristaByIdAsync(motoristaInput.Codigo);

            motorista.NombreCompleto = motoristaInput.NombreCompleto;
            motorista.TelefonoContacto = motoristaInput.TelefonoContacto;
            motorista.PlacasVehiculo = motoristaInput.PlacasVehiculo;
            motorista.TipoVehiculo = motoristaInput.TipoVehiculo;

            await _motoristaRepository.SaveChangesAsync();
            return motorista;
        }

        public async Task ActivarDesactivarMotoristaAsync(int idMotorista, bool estadoSetear)
        {
            //Saco al motorista con track
            Motorista motorista = await _motoristaRepository.GetMotoristaByIdAsync(idMotorista, true);
            motorista.Activo = estadoSetear;

            await _motoristaRepository.SaveChangesAsync();
        }

        public async Task<EntidadPaginada<Motorista>> BuscarMotoristasAsync(string nombre, int idCuenta, int paginaActual, int itemsPerPage)
        {
            int skip = (paginaActual - 1) * itemsPerPage;
            bool esNombreVacio = string.IsNullOrEmpty(nombre);


            Expression<Func<Motorista, bool>> where = x =>
                (x.IdCuenta == idCuenta) && (esNombreVacio || x.NombreCompleto.Contains(nombre));

            List<Motorista> listadoMotoristas = await _motoristaRepository.GetMotoristasAsync(where, skip, itemsPerPage);
            int contarMotoristas = await _motoristaRepository.CountMotoristasAsync(where);

            EntidadPaginada<Motorista> entidadPaginada = new EntidadPaginada<Motorista>()
            {
                Resultados = listadoMotoristas,
                PageSize = itemsPerPage,
                PaginaActual = paginaActual,
                TotalItems = contarMotoristas
            };

            return entidadPaginada;
        }

        public async Task<Motorista> GetMotoristaByUserAsync(string idUser)
        {
            List<Motorista> motoristas =
                await _motoristaRepository.GetMotoristasAsync(x => x.IdUsuario == idUser, 0, 1);

            return motoristas.FirstOrDefault();
        }

        public async Task<Motorista> ActualizarUbicacionMotoristaAsync(ActualizarUbicacionMotoristaInputDto actualizarUbicacionMotorista, string idUsuario)
        {
            //Primero sacamos el motorista
            Motorista motorista = await GetMotoristaByUserAsync(idUsuario);

            //Si el motorista no existe, kaboom!
            if (motorista == null)
            {
                throw new RuteoException(TipoError.MotoristaNoExiste, $"El motorista no existe para el usuario {idUsuario}");
            } 
            
            //Si el motorista esta inactivo, boom!
            if (motorista.Activo == false)
            {
                throw new RuteoException(TipoError.MotoristaInactivo, $"El motorista para el usuario: {idUsuario} esta inactivo");
            }

            DateTimeOffset fechaActualizacion = DateTimeOffset.Now;

            //Luego de pasar todas las validaciones, puedo actualizar los datos del motorista
            motorista.LatitudUltimaUbicacion = actualizarUbicacionMotorista.Latitud;
            motorista.LongitudUltimaUbicacion = actualizarUbicacionMotorista.Longitud;
            motorista.FechaActualizacionUbicacion = fechaActualizacion;

            //mando a marcar como modificada la entidad
            _motoristaRepository.MarcarMotoristaModificado(motorista);

            //Ahora, creamos un historico de ubicaciones
            HistoricoUbicacionMotorista historicoUbicacionMotorista = new HistoricoUbicacionMotorista()
            {
                IdMotorista = motorista.Codigo,
                FechaRegistroUbicacion = fechaActualizacion,
                Latitud = actualizarUbicacionMotorista.Latitud,
                Longitud = actualizarUbicacionMotorista.Longitud,
                Codigo = Guid.NewGuid().ToString()
            };

            await _historicoUbicacionMotorista.AgregarHistoricoAsync(historicoUbicacionMotorista);

            //Hacemos un solo save changos
            await _motoristaRepository.SaveChangesAsync();

            return motorista;
        }
    }
}

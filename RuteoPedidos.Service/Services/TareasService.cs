using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using RuteoPedidos.Core.DTO.Input.Tareas;
using RuteoPedidos.Core.DTO.Output.Ruteo.MapBox;
using RuteoPedidos.Core.DTO.Output.Tareas;
using RuteoPedidos.Core.Exceptions;
using RuteoPedidos.Core.Model;
using RuteoPedidos.Core.Model.Motoristas;
using RuteoPedidos.Core.Model.Tareas;
using RuteoPedidos.Core.Providers;
using RuteoPedidos.Core.Repositories;
using RuteoPedidos.Core.Services;

namespace RuteoPedidos.Service.Services
{
    public class TareasService: ITareasService
    {
        private readonly IMapper _mapper;
        private readonly ITareaRepository _tareaRepository;
        private readonly IDestinoRepository _destinoRepository;
        private readonly IVisitaRepository _visitaRepository;
        private readonly IRuteoProvider _ruteoProvider;
        private readonly IMotoristaRepository _motoristaRepository;

        public TareasService(IMapper mapper,
            ITareaRepository tareaRepository,
            IDestinoRepository destinoRepository,
            IVisitaRepository visitaRepository,
            IRuteoProvider ruteoProvider,
            IMotoristaRepository motoristaRepository)
        {
            _tareaRepository = tareaRepository;
            _destinoRepository = destinoRepository;
            _visitaRepository = visitaRepository;
            _ruteoProvider = ruteoProvider;
            _motoristaRepository = motoristaRepository;
            _mapper = mapper;
        }


        public async Task<Tarea> CrearTareaAsync(TareaInputDto tareaInput, bool generarCliente = false)
        {
            //Primero mandamos a crear la tarea
            Tarea tarea = _mapper.Map<TareaInputDto, Tarea>(tareaInput);
            tarea.IdMotoristaAsignado = tareaInput.IdMotorista;
            await _tareaRepository.AgregarTareaAsync(tarea);

            if (generarCliente)
            {
                Destino destino = new Destino()
                {
                    Codigo = 0,
                    TelefonoContacto = tareaInput.TelefonoContacto,
                    Activo = true,
                    Nombre = tareaInput.DestinoCliente,
                    Direccion = tareaInput.Indicaciones,
                    LongitudUbicacion = tareaInput.LongitudUbicacion,
                    LatitudUbicacion = tareaInput.LatitudUbicacion,
                    IdCuenta = tareaInput.IdCuenta
                };

                await _destinoRepository.AgregarDestinoAsync(destino);
            }

            await _tareaRepository.SaveChangesAsync();
            return tarea;
        }

        public async Task<EntidadPaginada<Destino>> BuscarDestinosAsync(string texto, int idCuenta, DateTimeOffset fechaDesde, DateTimeOffset? fechaHasta = null, int paginaActual = 1, int pageSize = 10)
        {
            int skip = (paginaActual - 1) * pageSize;
            bool textoVacio = string.IsNullOrEmpty(texto);

            Expression<Func<Destino, bool>> where = destino =>
                (destino.IdCuenta == idCuenta) &&
                (textoVacio || destino.Nombre.Contains(texto) || destino.Direccion.Contains(texto)) &&
                (destino.FechaIngreso >= fechaDesde && (fechaHasta == null || destino.FechaIngreso <= fechaHasta));

            List<Destino> destinos = await _destinoRepository.BuscarDestinosAsync(where, skip, pageSize);
            int totalItems = await _destinoRepository.CountDestinoAsync(where);

            EntidadPaginada<Destino> destinoEntidadPaginada = new EntidadPaginada<Destino>()
            {
                TotalItems = totalItems,
                PageSize = pageSize,
                PaginaActual = paginaActual,
                Resultados = destinos
            };

            return destinoEntidadPaginada;
        }

        public async Task<TareaOutputDto> GenerarTareaByDestinoAsync(int idDestino)
        {
            //Primero sacamos el destino
            Destino destino = await _destinoRepository.GetDestinoByIdAsync(idDestino);

            TareaOutputDto tareaOutput = _mapper.Map<Destino, TareaOutputDto>(destino);

            return tareaOutput;
        }

        public async Task EliminarTareaAsync(int idTarea)
        {
            //Primero sacamos la tarea, para ver su estado
            Tarea tarea = await _tareaRepository.GetTareaByIdAsync(idTarea);

            if (tarea.EstadoTarea != EstadoTarea.Pendiente)
            {
                throw new RuteoException(TipoError.EstadoInvalidoEliminacion, "El estado actual no es valido para realizar una eliminacion");
            }

            //Ahora, debo eliminar la tarea
            _tareaRepository.MarcarEliminarTarea(idTarea);

            //Hago un solo save changos
            await _tareaRepository.SaveChangesAsync();
        }

        public async Task AsignarTareaMotoristaAsync(int idTarea, int idMotorista)
        {
            //Saco la tarea
            Tarea tarea = await _tareaRepository.GetTareaByIdAsync(idTarea, true);

            //Si ya tiene asignado un motorista, reviento
            if (tarea.IdMotoristaAsignado != null)
            {
                throw new RuteoException(TipoError.TareaYaAsignada, $"No se puede asignar motorista, la tarea {idTarea} ya fue asignada al motorista {tarea.IdMotoristaAsignado.Value}");
            }

            //Seteo el motorista y la fecha de asignacion del mismo
            tarea.IdMotoristaAsignado = idMotorista;
            tarea.FechaAsignacionMotorista = DateTimeOffset.Now;

            await _tareaRepository.SaveChangesAsync();
        }

        public async Task DesasignarTareaMotoristaAsync(int idTarea, int idMotorista)
        {
            //Primero,  sacola tarea
            Tarea tarea = await _tareaRepository.GetTareaByIdAsync(idTarea, true);

            //Valido que el motorista asingado sea el que quieren quitar
            if (tarea.IdMotoristaAsignado == null || tarea.IdMotoristaAsignado.Value != idMotorista)
            {
                throw new RuteoException(TipoError.IdMotoristaInvalido,
                    $"El id del motorist que desea eliminar no coincide con el valor actual. Valor asignado: {tarea.IdMotoristaAsignado}, valor a quitar: {idMotorista}");
            }

            //Ahora, ya paso las validaciones, remuevo
            tarea.IdMotoristaAsignado = null;
            tarea.FechaAsignacionMotorista = null;

            await _tareaRepository.SaveChangesAsync();
        }

        public async Task<List<TareaOutputDto>> OrdenarTareasMotoristaAsync(int idMotorista, decimal latitudActual, decimal longitudActual, TipoOrdenTarea tipoOrden, int topPendientes = 10)
        {
            //Primero, saco las tareas pendientes del motorista
            List<Tarea> tareas =
                await _tareaRepository.GetTareasAsync(x => x.IdMotoristaAsignado == idMotorista && x.EstadoTarea == EstadoTarea.Pendiente, 0, int.MaxValue);

            //Saco las tareas del dia del motorista, un top de 10

            List<Tarea> tareasFinalizadas = await _tareaRepository.GetTareasAsync(
                x => x.IdMotoristaAsignado == idMotorista && x.EstadoTarea == EstadoTarea.Finalizada && (x.FechaUltimoCambioEstado != null && x.FechaUltimoCambioEstado.Value.Date == DateTimeOffset.Now.Date), 0, topPendientes);

            List<TareaOutputDto> tareasOutPut = new List<TareaOutputDto>();

            //Para cada tarea, debo ir a ver que ondas
            ConcurrentBag<TareaOutputDto> concurrentBagTareaOutput = new ConcurrentBag<TareaOutputDto>();

            IEnumerable<Task> tareasConsultarMapa = tareas.Select(async tarea =>
            {
                //Mando a llamar el api
                RespuestaMapBoxOutputDto respuestaMapBox = await _ruteoProvider.GetMapBoxDistanceAsync(latitudActual,
                    longitudActual, tarea.LatitudUbicacion, tarea.LongitudUbicacion);

                //Creo un objeto de respuesta
                TareaOutputDto tareaOutputDto = _mapper.Map<Tarea, TareaOutputDto>(tarea);
                tareaOutputDto.IdMotorista = idMotorista;
                tareaOutputDto.MinutosLlegada = Math.Round(respuestaMapBox.Routes.First().Duration / 60, 2);
                tareaOutputDto.KilometrosDistancia = Math.Round(respuestaMapBox.Routes.First().Distance / 1000, 2);
                tareaOutputDto.KilometrosDistanciaTexto = tareaOutputDto.KilometrosDistancia.ToString("0.####") + " km";
                tareaOutputDto.MinutosLlegadaTexto = tareaOutputDto.MinutosLlegada.ToString("0.####") + " min";

                concurrentBagTareaOutput.Add(tareaOutputDto);
            });

            //Espero que finalicen todas las tareas
            await Task.WhenAll(tareasConsultarMapa);

            //Agrego las tareas a la lista
            tareasOutPut.AddRange(concurrentBagTareaOutput);

            //Ordenamos segun nos indican
            switch (tipoOrden)
            {
                case TipoOrdenTarea.MasCercana:
                    tareasOutPut = tareasOutPut.OrderBy(x => x.KilometrosDistancia).ToList();
                    break;
                case TipoOrdenTarea.TiempoLlegada:
                    tareasOutPut = tareasOutPut.OrderBy(x => x.MinutosLlegada).ToList();
                    break;
            }

            //Loopeo para setear el orden
            for (int i = 0; i < tareasOutPut.Count; i++)
            {
                tareasOutPut[i].Orden = i + 1;
            }


            
            if (tareasFinalizadas.Any())
            {
                //Saco las visitas, si es que hay tareas finalizadas
                List<Visita> visitas = await _visitaRepository.GetVisitasByTareasAsync(tareasFinalizadas.Select(y => y.Codigo)
                    .ToArray());

                //Agrego las tareas finalizadas, si es que hay
                foreach (Tarea tareaFinalizada in tareasFinalizadas)
                {
                    TareaOutputDto tareaFinalizadaOutput = _mapper.Map<Tarea, TareaOutputDto>(tareaFinalizada);
                    tareaFinalizadaOutput.IdMotorista = idMotorista;

                    //Busco la visita dentro la lista de visitas
                    Visita visitaActual = visitas.FirstOrDefault(x => x.IdTarea == tareaFinalizada.Codigo);
                    tareaFinalizadaOutput.Visita = visitaActual == null ? null: _mapper.Map<Visita, VisitaOuputDto>(visitaActual);
                    tareasOutPut.Add(tareaFinalizadaOutput);
                }
            }

           

            return tareasOutPut;
        }

        public async Task<EntidadPaginada<Tarea>> GetTareasAsync(int[] motoristas, int idCuenta, DateTimeOffset fechaInicio, DateTimeOffset? fechaFin = null, int paginaActual = 1, int pageSize = 10)
        {
            bool noHayMotoristas = !motoristas.Any();
            int skip = (paginaActual - 1) * pageSize;

            Expression<Func<Tarea, bool>> where = tarea =>
                (tarea.IdCuenta == idCuenta) &&
                (noHayMotoristas || motoristas.Any(y => y == tarea.IdMotoristaAsignado)) &&
                (tarea.FechaIngreso >= fechaInicio && (fechaFin == null || tarea.FechaIngreso <= fechaFin));

            List<Tarea> tareas = await _tareaRepository.GetTareasAsync(where, skip, pageSize, true);
            int totalItems = await _tareaRepository.CountTareasAsync(where);

            EntidadPaginada<Tarea> entoEntidadPaginada = new EntidadPaginada<Tarea>()
            {
                PageSize = pageSize,
                TotalItems = totalItems,
                PaginaActual = paginaActual,
                Resultados = tareas
            };

            return entoEntidadPaginada;
        }

        public async Task<Visita> GenerarResultadoVisitaAsync(GenerarVisitaInputDto generarVisita)
        {
            //Primero verifico que no exista una visita para la tarea
            bool existeVisita = await _visitaRepository.ExisteVisitaParaTareaAsync(generarVisita.IdTarea);

            if (existeVisita)
            {
                throw new RuteoException(TipoError.VisitaYaGenerada, $"La visita para la tarea {generarVisita.IdTarea} ya fue generada");
            }

            //Ahora saco la tarea y valido tambien su estado
            Tarea tarea = await _tareaRepository.GetTareaByIdAsync(generarVisita.IdTarea, true);

            if (tarea == null)
            {
                throw new RuteoException(TipoError.TareaNoExiste, $"La tarea {generarVisita.IdTarea} no existe");
            }

            //Si la tarea no esta pendiente, reventamos
            if (tarea.EstadoTarea != EstadoTarea.Pendiente)
            {
                throw new RuteoException(TipoError.EstadoInvalidoVisita,
                    $"No se puede generar una visita para una tarea en estado {tarea.EstadoTarea}");
            }

            //En este punto es seguro generar la visita
            Visita visita = new Visita()
            {
                FechaIngreso = DateTimeOffset.Now,
                IdTarea = generarVisita.IdTarea,
                Comentario = generarVisita.Comentario,
                ResultadoVisita = generarVisita.ResultadoVisita,
                Codigo = string.IsNullOrEmpty(generarVisita.IdVisita) ? Guid.NewGuid().ToString(): generarVisita.IdVisita
            };

            //Mando a agregar la visita
            await _visitaRepository.AgregarVisitaAsync(visita);
            
            //Cambio el estado de la tarea
            tarea.EstadoTarea = EstadoTarea.Finalizada;
            tarea.FechaUltimoCambioEstado = DateTimeOffset.Now;

            //Ahora mandamos a hacer save changos
            await _tareaRepository.SaveChangesAsync();

            return visita;
        }

        public async Task<List<DashboardOuputDto>> ArmarDashboardAsync(int idCuenta, TipoOrdenTarea tipoOrden) 
        {

            List<TareaOutputDto> tareasOutPut = new List<TareaOutputDto>();

            //Primero se deben sacar los motoristas de la cuenta
            List<Motorista> motoristas =
                await _motoristaRepository.GetMotoristasAsync(x => x.Activo && x.IdCuenta == idCuenta, 0, int.MaxValue);

            int[] codigosMotoristas = motoristas.Select(x => x.Codigo).ToArray();

            //Ahora debemos sacar todas las tareas de todos los motoristas
            //Primero, saco las tareas pendientes del motorista
            List<Tarea> tareas =
                await _tareaRepository.GetTareasAsync(
                    x => x.IdMotoristaAsignado != null && codigosMotoristas.Any(p => p == x.IdMotoristaAsignado.Value) &&
                         x.EstadoTarea == EstadoTarea.Pendiente, 0, int.MaxValue);

            //Para cada tarea, debo ir a ver que ondas
            ConcurrentBag<TareaOutputDto> concurrentBagTareaOutput = new ConcurrentBag<TareaOutputDto>();

            List<Task> taskConsultas = new List<Task>();

            //Ahora, debo recorrer cada motorista y para cada motorista ordenar sus tareas
            foreach (Motorista motorista in motoristas.Where(y => y.LatitudUltimaUbicacion != null && y.LongitudUltimaUbicacion != null))
            {
                //Solo procedo  si tengo su ubicacion
                if (motorista.LatitudUltimaUbicacion.HasValue && motorista.LongitudUltimaUbicacion.HasValue)
                {
                    //Saco las tareas del motorista
                    List<Tarea> tareasMotorista = tareas.Where(x => x.IdMotoristaAsignado == motorista.Codigo).ToList();

                    IEnumerable<Task> tareasConsultarMapa = tareasMotorista.Select(async tarea =>
                    {
                        //Mando a llamar el api
                        RespuestaMapBoxOutputDto respuestaMapBox = await _ruteoProvider.GetMapBoxDistanceAsync(motorista.LatitudUltimaUbicacion.Value,
                            motorista.LongitudUltimaUbicacion.Value, tarea.LatitudUbicacion, tarea.LongitudUbicacion);

                        //Creo un objeto de respuesta
                        TareaOutputDto tareaOutputDto = _mapper.Map<Tarea, TareaOutputDto>(tarea);
                        tareaOutputDto.IdMotorista = motorista.Codigo;
                        tareaOutputDto.MinutosLlegada = Math.Round(respuestaMapBox.Routes.First().Duration / 60, 2);
                        tareaOutputDto.KilometrosDistancia = Math.Round(respuestaMapBox.Routes.First().Distance / 1000, 2);
                        tareaOutputDto.KilometrosDistanciaTexto = tareaOutputDto.KilometrosDistancia.ToString("0.####") + " km";
                        tareaOutputDto.MinutosLlegadaTexto = tareaOutputDto.MinutosLlegada.ToString("0.####") + " min";
                        tareaOutputDto.PuntosRuteo = GetCoordenadasMapbox(respuestaMapBox);

                        concurrentBagTareaOutput.Add(tareaOutputDto);
                    });

                    taskConsultas.AddRange(tareasConsultarMapa);
                }

            }

            //Espero que finalicen todas las tareas
            await Task.WhenAll(taskConsultas);

            //En este punto ya fui a consultar todas las tareas de todos los motoristas

            //Agrego las tareas a la lista
            tareasOutPut.AddRange(concurrentBagTareaOutput);

            List<DashboardOuputDto> dashboardOuputs = new List<DashboardOuputDto>();

            //Procedo a armar la ruta para cada motorista con en base en los datos de tareas
            foreach (Motorista motorista in motoristas)
            {
                List<TareaOutputDto> tareasMotoristaActual = new List<TareaOutputDto>();

                switch (tipoOrden)
                {
                    case TipoOrdenTarea.MasCercana:
                        tareasMotoristaActual = tareasOutPut.Where(y => y.IdMotorista == motorista.Codigo).OrderBy(x => x.KilometrosDistancia).ToList();
                        break;
                    case TipoOrdenTarea.TiempoLlegada:
                        tareasMotoristaActual = tareasOutPut.Where(y => y.IdMotorista == motorista.Codigo).OrderBy(x => x.MinutosLlegada).ToList();
                        break;
                }

                DashboardOuputDto dashboardOuput = new DashboardOuputDto()
                {
                    IdMotorista = motorista.Codigo,
                    TareasMotorista = tareasMotoristaActual,
                    TipoOrdenTarea = tipoOrden,
                    LatitudMotorista = motorista.LatitudUltimaUbicacion ?? 0,
                    LongitudMotorista = motorista.LongitudUltimaUbicacion ?? 0,
                    NombreMotorista = motorista.NombreCompleto
                };

                dashboardOuputs.Add(dashboardOuput);
            }

            return dashboardOuputs;

        }

        public async Task<RutaMotoristaTareaOuputDto> CalcularRutaMotoristaTareaAsync(int idMotorista, int idTarea)
        {
            //Primero sacamos el motorista
            Motorista motorista = await _motoristaRepository.GetMotoristaByIdAsync(idMotorista);

            //Validamos que si no tenemos la ubicacion, reventamos
            if (motorista.LongitudUltimaUbicacion == null || motorista.LatitudUltimaUbicacion == null)
            {
                throw new RuteoException(TipoError.MotoristaSinUbicacion,
                    "El motorista no posee los datos de ubicacion");
            }

            //Sacamos la tarea
            Tarea tarea = await _tareaRepository.GetTareaByIdAsync(idTarea);

            //Validamos que la tarea si este asignada al motorista que nos mandan
            if (tarea.IdMotoristaAsignado == null || tarea.IdMotoristaAsignado.Value != idMotorista)
            {
                throw new RuteoException(TipoError.IdMotoristaInvalido, "La tarea no tiene asignado al motorista que nos envian");
            }

            //En este punto, es valido el estado de motorista y tarea, calculo la ruta
            RespuestaMapBoxOutputDto mapBoxOutput = await _ruteoProvider.GetMapBoxDistanceAsync(
                motorista.LatitudUltimaUbicacion.Value, motorista.LongitudUltimaUbicacion.Value, tarea.LatitudUbicacion,
                tarea.LongitudUbicacion);

            decimal[,] coordenadas = GetCoordenadasMapbox(mapBoxOutput);

            RutaMotoristaTareaOuputDto ruta = new RutaMotoristaTareaOuputDto()
            {
                LatitudInicial = motorista.LatitudUltimaUbicacion.Value,
                LongitudInicial = motorista.LongitudUltimaUbicacion.Value,
                LatitudFinal = tarea.LatitudUbicacion,
                LongitudFinal = tarea.LongitudUbicacion,
                IdMotorista = idMotorista,
                IdTarea = idTarea,
                PuntosRuta = coordenadas
            };

            return ruta;
        }

        private decimal[,] GetCoordenadasMapbox(RespuestaMapBoxOutputDto respuestaMapBox)
        {
            decimal[,] coordenadas = new decimal[respuestaMapBox.Routes.First().Geometry.Coordinates.Count, 2];

            for (int i = 0; i < coordenadas.GetLength(0); i++)
            {
                coordenadas[i, 0] = (decimal)respuestaMapBox.Routes.First().Geometry.Coordinates[i][1];
                coordenadas[i, 1] = (decimal)respuestaMapBox.Routes.First().Geometry.Coordinates[i][0];
            }

            return coordenadas;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RuteoPedidos.Core.Model
{
    public enum TipoVehiculo
    {
        Motocicleta,
        CarroParticular
    }

    public enum EstadoTarea
    {
        Pendiente,
        Finalizada,
        Cancelada
    }

    public enum ResultadoVisita
    {
        [Display(Name = "Entregado")]
        Entregado,
        [Display(Name = "Problemas con la entrega")]
        ProblemasEntrega,
        [Display(Name = "Ubicacion no encontrada")]
        UbicacionNoEncontrada,
        [Display(Name = "Otros")]
        Otro
    }

    public enum TipoOrdenTarea
    {
        MasCercana,
        TiempoLlegada
    }

    public enum TipoError
    {
        EstadoInvalidoEliminacion,
        TareaYaAsignada,
        TareaNoExiste,
        IdMotoristaInvalido,
        VisitaYaGenerada,
        EstadoInvalidoVisita,
        MotoristaNoExiste,
        MotoristaInactivo,
        MotoristaConUsusario,
        MotoristaSinUbicacion
    }

    public enum FiltroEstadoUsuario
    {
        Todos,
        Activo,
        Inactivo
    }
}

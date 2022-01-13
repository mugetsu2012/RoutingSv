using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using RuteoPedidos.Core.DTO.Input.Tareas;
using RuteoPedidos.Core.DTO.Output.Tareas;
using RuteoPedidos.Core.Model.Tareas;
using RuteoPedidos.WebAdmin.Models;

namespace RuteoPedidos.WebAdmin.MapperProfiles
{
    public class TareaProfile: Profile
    {
        public TareaProfile()
        {
            CreateMap<TareaInputDto, Tarea>().ReverseMap();
            CreateMap<Tarea, TareaOutputDto>().ReverseMap();
            CreateMap<Destino, TareaOutputDto>().ReverseMap();
            CreateMap<Visita, VisitaOuputDto>().ReverseMap();
            CreateMap<CrearTareaVm, TareaInputDto>().ReverseMap();

        }
    }
}

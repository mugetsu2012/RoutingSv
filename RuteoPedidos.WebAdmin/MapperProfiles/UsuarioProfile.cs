using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using RuteoPedidos.Core.DTO.Input;
using RuteoPedidos.Core.Model;
using RuteoPedidos.WebAdmin.Models;

namespace RuteoPedidos.WebAdmin.MapperProfiles
{
    public class UsuarioProfile: Profile
    {
        public UsuarioProfile()
        {
            CreateMap<UsuarioInputDto, Usuario>().ReverseMap();
            CreateMap<CrearUsuarioVm, UsuarioInputDto>().ReverseMap();
        }
    }
}

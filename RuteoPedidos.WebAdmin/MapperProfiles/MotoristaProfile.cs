using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using RuteoPedidos.Core.DTO.Input.Motoristas;
using RuteoPedidos.Core.Model.Motoristas;

namespace RuteoPedidos.WebAdmin.MapperProfiles
{
    public class MotoristaProfile: Profile
    {
        public MotoristaProfile()
        {
            CreateMap<MotoristaInputDto, Motorista>().ReverseMap();
        }
    }
}

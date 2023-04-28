using AutoMapper;
using Ostium.BeforeIDie.Domain.Dto;
using Ostium.BeforeIDie.Domain.Entities;

namespace Ostium.BeforeIDie.IoC
{
    public class AutoMapperConfig:Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<SonhadorEntity, SonhadorDto>().ReverseMap();
            CreateMap<SonhoEntity, SonhoDto>().ReverseMap();
            CreateMap<ModeloTrilhaEntity, ModeloTrilhaDto>().ReverseMap();
            CreateMap<TrilhaEntity, TrilhaDto>().ReverseMap();
        }
    }
}
using AutoMapper;
using Ostium.BeforeIDie.Domain.Dto;
using Ostium.BeforeIDie.Domain.Entities;
using System.Diagnostics.CodeAnalysis;

namespace Ostium.BeforeIDie.IoC
{
    [ExcludeFromCodeCoverageAttribute]
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
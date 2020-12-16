using AutoMapper;
using Ostium.BeforeIDie.API.Model.Dto;
using Ostium.BeforeIDie.API.Model.Entities;

namespace Ostium.BeforeIDie.API.Configurations
{
    public class AutoMapperConfig:Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<SonhadorEntity, SonhadorDto>().ReverseMap();
        }
    }
}

using ApiOnion104.Application.DTOs.Colors;
using ApiOnion104.Domain.Entities;
using AutoMapper;

namespace ApiOnion104.Application.MappingProfiles
{
    public class ColorProfile : Profile
    {
        public ColorProfile()
        {
            CreateMap<Color, ColorItemDto>().ReverseMap();
            CreateMap<Color, ColorGetDto>().ReverseMap();
            CreateMap<ColorUpdateDto, Color>();

        }
    }
}

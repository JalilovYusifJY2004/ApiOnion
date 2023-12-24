using AutoMapper;
using ApiOnion104.Application.DTOs.Tag;
using ApiOnion104.Domain.Entities;

namespace ProniaOnionAB104.Application.MappingProfiles
{
    public class TagProfile:Profile
    {
        public TagProfile()
        {
            CreateMap<Tag, TagItemDto>();
            CreateMap<TagCreateDto, Tag>();
        }
    }
}

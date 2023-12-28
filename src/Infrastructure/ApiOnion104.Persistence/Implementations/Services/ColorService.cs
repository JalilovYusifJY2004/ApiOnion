using ApiOnion104.Application.Abstractions.Repositories;
using ApiOnion104.Application.Abstractions.Services;
using ApiOnion104.Application.DTOs.Colors;
using ApiOnion104.Domain.Entities;
using ApiOnion104.Persistence.Implementations.Repositories;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ApiOnion104.Persistence.Implementations.Services
{
    public class ColorService : IColorService
    {
        private readonly IColorRepositoy _repository;
        private readonly IMapper _mapper;
        public ColorService(IMapper mapper, IColorRepositoy repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task CreateAsync(ColorCreateDto colorCreateDto)
        {
            Color color = new Color
            {
                Name = colorCreateDto.Name
            };
            await _repository.AddAsync(color);
            await _repository.SaveChangesAsync();
        }

        public async Task<ICollection<ColorItemDto>> GetAllAsync(int page, int take)
        {
            ICollection<Color> colors = await _repository.GetAllWhere(skip: (page - 1) * take, take: take, isTracking: false, ignoreQuery: true).ToListAsync();

            ICollection<ColorItemDto> colorItemDtos = new List<ColorItemDto>();

            foreach (Color color in colors)
            {
                colorItemDtos.Add(new ColorItemDto(color.Id, color.Name));
            }
            return colorItemDtos;
        }
        public async Task UpdateAsync(int id, ColorUpdateDto colorUpdateDto)
        {
            Color color = await _repository.GetByIdAsync(id);

            if (color is null) throw new Exception("Not found");

            color = _mapper.Map(colorUpdateDto, color);

            _repository.Update(color);
            await _repository.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            Color color = await _repository.GetByIdAsync(id);

            if (color is null) throw new Exception("Not found");

            _repository.Delete(color);
            await _repository.SaveChangesAsync();
        }
        public async Task SoftDeleteAsync(int id)
        {
            Color color = await _repository.GetByIdAsync(id);

            if (color is null) throw new Exception("Not found");
            _repository.SoftDelete(color);
            await _repository.SaveChangesAsync();
        }

        public async Task<ColorGetDto> GetAsync(int id)
        {
            Color color = await _repository.GetByIdAsync(id);

            if (color is null) throw new Exception("Not found");
            return new ColorGetDto(color.Id, color.Name);
        }
    }
}

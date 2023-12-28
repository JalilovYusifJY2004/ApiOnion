
using ApiOnion104.Application.DTOs.Colors;

namespace ApiOnion104.Application.Abstractions.Services
{
    public interface IColorService
    {
        Task<ICollection<ColorItemDto>> GetAllAsync(int page, int take);
        Task<ColorGetDto> GetAsync(int id);
        Task CreateAsync(ColorCreateDto colorCreateDto);
        Task UpdateAsync(int id, ColorUpdateDto colorUpdateDto);
        Task DeleteAsync(int id);
        Task SoftDeleteAsync(int id);
    }
}

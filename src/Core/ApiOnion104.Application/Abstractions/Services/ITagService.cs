using ApiOnion104.Application.DTOs.Tag;

namespace Application.Abstractions.Services
{
    public interface ITagService
    {
        Task<ICollection<TagItemDto>> GetAllAsync(int page, int take);
        //Task<GetTagDto> GetAsync(int id);
        Task CreateAsync(TagCreateDto tagDto);
        Task UpdateAsync(int id, TagUpdateDto tagDto);
        Task DeleteAsync(int id);
        Task SoftDeleteAsync(int id);
    }
}

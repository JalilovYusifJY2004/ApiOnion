using A.Application.Abstractions.Repositories;
using ApiOnion104.Application.DTOs.Tag;
using ApiOnion104.Domain.Entities;
using Application.Abstractions.Services;
using Microsoft.EntityFrameworkCore;


namespace ApiOnion104.Persistence.Implementations.Services
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _repository;

        public TagService(ITagRepository repository)
        {
            _repository = repository;
        }

        public async Task CreateAsync(TagCreateDto tagCreateDto)
        {
            Tag tag = new Tag
            {
                Name = tagCreateDto.Name
            };
            await _repository.AddAsync(tag);
            await _repository.SaveChangesAsync();
        }

        public async Task<ICollection<TagItemDto>> GetAllAsync(int page, int take)
        {
            ICollection<Tag> tags = await _repository.GetAllWhere(skip: (page - 1) * take, take: take, isTracking: false, ignoreQuery: true).ToListAsync();

            ICollection<TagItemDto> tagItemDtos = new List<TagItemDto>();

            foreach (Tag tag in tags)
            {
                tagItemDtos.Add(new TagItemDto(tag.Id, tag.Name));
            }
            return tagItemDtos;
        }
        public async Task UpdateAsync(int id, TagUpdateDto tagUpdateDto)
        {
            Tag tag = await _repository.GetByIdAsync(id);

            if (tag is null) throw new Exception("Not found");

            tag.Name = tagUpdateDto.Name;

            _repository.Update(tag);
            await _repository.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            Tag tag = await _repository.GetByIdAsync(id);

            if (tag is null) throw new Exception("Not found");

            _repository.Delete(tag);
            await _repository.SaveChangesAsync();
        }
        public async Task SoftDeleteAsync(int id)
        {
            Tag tag = await _repository.GetByIdAsync(id);
            if (tag is null) throw new Exception("Not found");
            //_repository.SoftDeleteAs(tag);
            await _repository.SaveChangesAsync();
        }

        //public async Task<CategoryItemDto> GetAsync(int id)
        //{
        //    Category category = await _repository.GetByIdAsync(id);

        //    if (category is null) throw new Exception("Not found");

        //    return new CategoryItemDto()
        //    {
        //        Id = category.Id,
        //        Name = category.Name,
        //    };
        //}
    }
}

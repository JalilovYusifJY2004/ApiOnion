using ApiOnion104.Application.Abstractions.Repositories;
using ApiOnion104.Application.Abstractions.Services;
using ApiOnion104.Application.DTOs.Products;
using ApiOnion104.Domain.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiOnion104.Persistence.Implementations.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepositoy _repositoy;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IColorRepositoy _colorRepositoy;
        private readonly IMapper _mapper;

        public ProductService(IProductRepositoy repositoy, ICategoryRepository categoryRepository, IColorRepositoy colorRepositoy, IMapper mapper)
        {
            _repositoy = repositoy;
            _categoryRepository = categoryRepository;
            _colorRepositoy = colorRepositoy;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ProductItemDto>> GettAllPaginated(int page, int take)
        {
            IEnumerable<ProductItemDto> dtos = _mapper.Map<IEnumerable<ProductItemDto>>(await _repositoy.GetAllWhere(skip: (page - 1) * take, take: take, isTracking: false, ignoreQuery: false).ToArrayAsync());
            return dtos;
        }
        public async Task<ProductGetDto> GetByIdAsync(int id)
        {
            Product product = await _repositoy.GetByIdAsync(id, includes: nameof(Product.Category));
            ProductGetDto dto = _mapper.Map<ProductGetDto>(product);
            return dto;
        }
        public async Task CreateAsync(ProductCreateDto dto)
        {
            if (await _repositoy.IsExistAsync(p => p.Name == dto.Name)) throw new Exception("Pro with name already exists");
            if (!await _categoryRepository.IsExistAsync(c => c.Id == dto.CategorId)) throw new Exception("dont");
            Product product = _mapper.Map<Product>(dto);
            product.ProductColors = new List<ProductColor>();
            foreach (var colorId in dto.ColorIds)
            {
                if (!await _colorRepositoy.IsExistAsync(c => c.Id == colorId)) throw new Exception("dont");
                product.ProductColors.Add(new ProductColor
                {
                    ColorId = colorId
                });
                
            }
            await _repositoy.AddAsync(product);
            await _repositoy.SaveChangesAsync();
        }
    }
}

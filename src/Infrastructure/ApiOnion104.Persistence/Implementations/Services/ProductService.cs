﻿using ApiOnion104.Application.Abstractions.Repositories;
using ApiOnion104.Application.Abstractions.Services;
using ApiOnion104.Application.DTOs.Products;
using ApiOnion104.Domain.Entities;
using ApiOnion104.Persistence.Implementations.Repositories.Generic;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Product = ApiOnion104.Domain.Entities.Product;

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
            Domain.Entities.Product product = await _repositoy.GetByIdAsync(id, includes: nameof(Product.Category));
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
        public async Task UpdateAsync(int id, ProductUpdateDto dto)
        {
            Product existed = await _repositoy.GetByIdAsync(id, includes: nameof(Product.ProductColors));
            if (existed is null) throw new Exception("dont");
            if (dto.CategorId != existed.CategoryId)
            {
                if (!await _categoryRepository.IsExistAsync(c => c.Id == dto.CategorId)) throw new Exception("dont");
            }
            existed = _mapper.Map(dto, existed);
            existed.ProductColors = existed.ProductColors.Where(pc => dto.ColorIds.Any(colorId => pc.ColorId == colorId)).ToList();

            foreach (var cId in dto.ColorIds)
            {
                if (!await _colorRepositoy.IsExistAsync(c => c.Id == cId)) throw new Exception("dont");
                if(!existed.ProductColors.Any(pc=>pc.ColorId== cId))
                {
                    existed.ProductColors.Add(new ProductColor { ColorId= cId });
                }
            }
            _repositoy.Update(existed);
            await _repositoy.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Product product = await _repositoy.GetByIdAsync(id);
            if (product is null) throw new Exception("Not found");
            _repositoy.Delete(product);
            await _repositoy.SaveChangesAsync();
        }

        public async Task SoftDeleteAsync(int id)
        {
            Product product = await _repositoy.GetByIdAsync(id);
            if (product is null) throw new Exception("Not found");
            _repositoy.SoftDelete(product);
            await _repositoy.SaveChangesAsync();
        }
    }
    }


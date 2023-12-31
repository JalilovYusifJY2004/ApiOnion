﻿using ApiOnion104.Application.Abstractions.Services;
using ApiOnion104.Application.DTOs.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiOnion104.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductsController(IProductService service)
        {
         _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> Get(int page = 1, int take = 3)
        {
            return Ok(await _service.GettAllPaginated(page, take));
        }
        [HttpGet("id")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _service.GetByIdAsync(id));
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromForm]ProductCreateDto dto)
        {
            await _service.CreateAsync(dto);
            return StatusCode(StatusCodes.Status201Created);
        }
        [HttpPut("id")]
        public async Task<IActionResult> Put(int id, [FromForm] ProductUpdateDto dto)
        {
            if (id <= 0) return BadRequest();
            await _service.UpdateAsync(id, dto);
            return NoContent();
           
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);
            await _service.SoftDeleteAsync(id);
            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}

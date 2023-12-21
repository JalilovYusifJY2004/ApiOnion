using ApiOnion104.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiOnion104.Application.DTOs.Products
{
    public record ProductItemDto(int Id,string Name,decimal Price,string SKU,string? Description);
   
}

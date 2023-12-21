using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiOnion104.Application.DTOs.Products
{
    public record ProductCreateDto(string Name, decimal Price, string SKU, string? Description,int CategorId,ICollection<int> ColorIds);
  
}

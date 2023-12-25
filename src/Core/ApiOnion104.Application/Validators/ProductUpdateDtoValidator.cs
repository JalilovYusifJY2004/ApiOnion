using ApiOnion104.Application.DTOs.Products;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiOnion104.Application.Validators
{
    public class ProductUpdateDtoValidator:AbstractValidator<ProductUpdateDto>
    {
        public ProductUpdateDtoValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("Name is important").MaximumLength(100).WithMessage("Name may contain max 100 charects").MaximumLength(2).WithMessage("Name may contain min 2 charects");
            RuleFor(p => p.SKU).NotEmpty().MaximumLength(10);


            RuleFor(p => p.Price).NotEmpty()/*.Must(p=>p>10&p<99999.99m)*/.LessThanOrEqualTo(999999.99m).GreaterThanOrEqualTo(10)/*.Must(CheckPrice)*/;
            RuleFor(p => p.Description).MaximumLength(1000);
            RuleFor(x => x.CategorId).Must(c => c > 0);
            RuleForEach(x => x.ColorIds).Must(c => c > 0);
            RuleFor(x => x.ColorIds).NotNull();
        }
    }
}

using ApiOnion104.Application.DTOs.Colors;
using FluentValidation;

namespace ApiOnion104.Application.Validators
{
    internal class ColorCreateDtoValidator : AbstractValidator<ColorCreateDto>
    {
        public ColorCreateDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50).MinimumLength(5);
        }
    }
}

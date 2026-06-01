using FluentValidation;
using FutStats.Models;

namespace FutStats.Validators
{
    public class TimeValidator : AbstractValidator<Time>
    {
        public TimeValidator()
        {
            RuleFor(t => t.Nome)
                .NotEmpty().WithMessage("O nome do time é obrigatório.")
                .MinimumLength(3).WithMessage("O nome do time deve ter pelo menos 3 caracteres.");
        }
    }
}
using FluentValidation;
using FutStats.Models;

namespace FutStats.Validators
{
    public class PartidaValidator : AbstractValidator<Partida>
    {
        public PartidaValidator()
        {
            // Validações do Time da Casa
            RuleFor(p => p.IdTimeCasa)
                .GreaterThan(0).WithMessage("O ID do time da casa é inválido.")
                .NotEqual(p => p.IdTimeFora).WithMessage("Um time não pode jogar contra si mesmo!");

            // Validações do Time de Fora
            RuleFor(p => p.IdTimeFora)
                .GreaterThan(0).WithMessage("O ID do time de fora é inválido.");

            // Validações dos Placares (impossível ter gol negativo)
            RuleFor(p => p.PlacarTimeCasa)
                .GreaterThanOrEqualTo(0).WithMessage("O placar do time da casa não pode ser negativo.");

            RuleFor(p => p.PlacarTimeFora)
                .GreaterThanOrEqualTo(0).WithMessage("O placar do time de fora não pode ser negativo.");
        }
    }
}
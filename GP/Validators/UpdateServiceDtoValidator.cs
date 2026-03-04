using FluentValidation;
using GP.Contracts;

namespace GP.Validators
{
    /// <summary>
    /// Validator dla UpdateServiceDto.
    /// Definiuje reguły walidacji przy edycji usługi.
    /// </summary>
    public class UpdateServiceDtoValidator : AbstractValidator<UpdateServiceDto>
    {
        public UpdateServiceDtoValidator()
        {
            // Name - wymagane, 3-255 znaków
            RuleFor(x => x.Name)
                .NotEmpty()
                    .WithMessage("Nazwa usługi jest wymagana")
                .MinimumLength(3)
                    .WithMessage("Nazwa musi mieć minimum 3 znaki")
                .MaximumLength(255)
                    .WithMessage("Nazwa może mieć maximum 255 znaków");

            // Description - wymagane, minimum 10 znaków
            RuleFor(x => x.Description)
                .NotEmpty()
                    .WithMessage("Opis usługi jest wymagany")
                .MinimumLength(10)
                    .WithMessage("Opis musi mieć minimum 10 znaków");

            // PriceFrom - musi być > 0
            RuleFor(x => x.PriceFrom)
                .GreaterThan(0)
                    .WithMessage("Cena musi być większa niż 0");

            // IsActive - musi być boolean (zawsze spełniony)
            RuleFor(x => x.IsActive)
                .NotNull();

            RuleFor(x => x.Category)
                .NotEmpty()
                .Must(c => c.ToLower() is "nails" or "cosmetology")
                .WithMessage("Category musi być 'nails' albo 'cosmetology'.");
        }
    }
}

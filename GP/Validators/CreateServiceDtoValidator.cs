using FluentValidation;
using GP.Contracts;

namespace GP.Validators
{
    /// <summary>
    /// Validator dla CreateServiceDto.
    /// Definiuje reguły walidacji przy tworzeniu nowej usługi.
    /// </summary>
    public class CreateServiceDtoValidator : AbstractValidator<CreateServiceDto>
    {
        public CreateServiceDtoValidator()
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
        }
    }
}

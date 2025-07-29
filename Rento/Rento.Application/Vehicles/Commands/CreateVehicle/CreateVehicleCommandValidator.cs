using FluentValidation;
using Rento.Application.Common.Interfaces.Persistence;

namespace Rento.Application.Vehicles.Commands.CreateVehicle
{
    public class CreateVehicleCommandValidator : AbstractValidator<CreateVehicleCommand>
    {
        public CreateVehicleCommandValidator(IVehicleRepository repository)
        {
            RuleFor(x => x.Brand).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Model).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Year).InclusiveBetween(1990, DateTime.UtcNow.Year + 1);
            RuleFor(x => x.RegistrationNumber)
                .NotEmpty()
                .MaximumLength(20)
                .MustAsync(async (reg, ct) =>
                    !(await repository.ExistsWithRegistrationAsync(reg)))
                .WithMessage("Vehicle with that registration already exists.");

            RuleFor(x => x.ChassisNumber)
                .NotEmpty()
                .MaximumLength(50)
                .MustAsync(async (chas, ct) =>
                    !(await repository.ExistsWithChassisAsync(chas)))
                .WithMessage("Chassis number already exists.");
            RuleFor(x => x.FuelType).NotEmpty().MaximumLength(50);
            RuleFor(x => x.DoorsNumber).InclusiveBetween(2, 6);
            RuleFor(x => x.SeatsNumber).InclusiveBetween(2, 9);
            RuleFor(x => x.Price).GreaterThan(0);
            RuleFor(x => x.OwnerId).GreaterThan(0);
        }
    }
}

using FluentValidation;
using Rento.Application.Common.Interfaces.Persistence;

namespace Rento.Application.Vehicles.Commands.UpdateVehicle
{
    public class UpdateVehicleCommandValidator : AbstractValidator<UpdateVehicleCommand>
    {
        public UpdateVehicleCommandValidator(IVehicleRepository vehicleRepository, IUserRepository userRepository)
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.Brand).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Model).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Year).InclusiveBetween(1990, DateTime.UtcNow.Year + 1);
            RuleFor(x => x.RegistrationNumber)
                .NotEmpty()
                .MaximumLength(20)
                .MustAsync(async (reg, ct) =>
                    !(await vehicleRepository.ExistsWithRegistrationAsync(reg)))
                .WithMessage("Vehicle with that registration already exists.");
            RuleFor(x => x.ChassisNumber)
                .NotEmpty()
                .MaximumLength(50)
                .MustAsync(async (chas, ct) =>
                    !(await vehicleRepository.ExistsWithChassisAsync(chas)))
                .WithMessage("Chassis number already exists.");
            RuleFor(x => x.FuelType).NotEmpty().MaximumLength(50);
            RuleFor(x => x.DoorsNumber).InclusiveBetween(2, 6);
            RuleFor(x => x.SeatsNumber).InclusiveBetween(2, 9);
            RuleFor(x => x.Price).GreaterThan(0);
            RuleFor(x => x.OwnerId)
            .MustAsync(async (ownerId, ct) =>
                await userRepository.ExistsByIdAsync(ownerId, ct))
            .WithMessage("Owner doesn't exist.");
        }
    }
}

using ErrorOr;
using MediatR;
using Rento.Application.Common.Interfaces.Persistence;
using Rento.Domain.Entities;

namespace Rento.Application.Vehicles.Commands.AddVehicleUnavailability
{
    public class AddVehicleUnavailabilityCommandHandler
    : IRequestHandler<AddVehicleUnavailabilityCommand, ErrorOr<Success>>
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IReservationRepository _reservationRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddVehicleUnavailabilityCommandHandler(
            IVehicleRepository vehicleRepository,
            IReservationRepository reservationRepository,
            IUnitOfWork unitOfWork)
        {
            _vehicleRepository = vehicleRepository;
            _reservationRepository = reservationRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<Success>> Handle(
            AddVehicleUnavailabilityCommand request,
            CancellationToken cancellationToken)
        {
            var now = DateTime.UtcNow.Date;

            if (request.EndDate < request.StartDate)
            {
                return Error.Validation("EndDate", "Datum završetka ne može biti prije početnog.");
            }

            if (request.EndDate < now)
            {
                return Error.Validation("EndDate", "Nedostupnost ne može biti u prošlosti.");
            }

            var reservationRanges = await _reservationRepository
                .GetBusyDateRangesForVehicleAsync(request.VehicleId, now);

            var unavailabilityRanges = await _vehicleRepository
                .GetUnavailableDateRangesAsync(request.VehicleId, now);

            var overlaps = reservationRanges
                .Concat(unavailabilityRanges)
                .Any(r =>
                    request.StartDate <= r.EndDate &&
                    request.EndDate >= r.StartDate
                );

            if (overlaps)
            {
                return Error.Conflict(
                    "DateOverlap",
                    "Odabrani raspon se preklapa s postojećom rezervacijom ili nedostupnošću.");
            }

            var entity = new VehicleUnavailability
            {
                VehicleId = request.VehicleId,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Reason = request.Reason
            };

            _vehicleRepository.AddUnavailability(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success;
        }
    }
}

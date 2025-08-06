using MediatR;
using Rento.Application.Admin.Common;
using Rento.Application.Common.Interfaces.Persistence;

namespace Rento.Application.Admin.Queries.GetAllReservations
{
    public class GetAllReservationsForAdminQueryHandler
    : IRequestHandler<GetAllReservationsForAdminQuery, List<AdminReservationResponse>>
    {
        private readonly IReservationRepository _reservationRepository;

        public GetAllReservationsForAdminQueryHandler(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public async Task<List<AdminReservationResponse>> Handle(GetAllReservationsForAdminQuery request, CancellationToken cancellationToken)
        {
            var reservations = await _reservationRepository.GetAllWithUserAndVehicleAsync();

            return reservations.Select(r => new AdminReservationResponse
            {
                Id = r.Id,
                VehicleId = r.VehicleId,
                VehicleName = $"{r.Vehicle.Brand} {r.Vehicle.Model}",
                UserId = r.UserId,
                UserName = $"{r.User.FirstName} {r.User.LastName}",
                StartDate = r.StartDate,
                EndDate = r.EndDate,
                Status = r.Status.ToString()
            }).ToList();
        }
    }

}

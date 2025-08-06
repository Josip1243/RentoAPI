using MediatR;
using Rento.Application.Common.Interfaces.Persistence;

namespace Rento.Application.Reviews.Queries.CanUserReview
{
    public class CanUserReviewQueryHandler : IRequestHandler<CanUserReviewQuery, bool>
    {
        private readonly IReservationRepository _reservationRepository;

        public CanUserReviewQueryHandler(
            IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public async Task<bool> Handle(CanUserReviewQuery request, CancellationToken cancellationToken)
        {
            return await _reservationRepository.HasCompletedReservation(request.UserId, request.VehicleId);
        }
    }

}

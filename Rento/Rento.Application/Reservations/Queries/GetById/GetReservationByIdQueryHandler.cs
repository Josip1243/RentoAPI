using ErrorOr;
using MapsterMapper;
using MediatR;
using Rento.Application.Common.Interfaces.Persistence;
using Rento.Application.Reservations.Common;
using Rento.Domain.Common.Errors;

namespace Rento.Application.Reservations.Queries.GetById
{
    public class GetReservationByIdQueryHandler
    : IRequestHandler<GetReservationByIdQuery, ErrorOr<ReservationResponse>>
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IMapper _mapper;

        public GetReservationByIdQueryHandler(IReservationRepository reservationRepository, IMapper mapper)
        {
            _reservationRepository = reservationRepository;
            _mapper = mapper;
        }

        public async Task<ErrorOr<ReservationResponse>> Handle(GetReservationByIdQuery request, CancellationToken cancellationToken)
        {
            var reservation = await _reservationRepository.GetByIdAsync(request.Id, cancellationToken);

            if (reservation is null)
            {
                return Errors.Reservation.ReservationNotFound;
            }

            return _mapper.Map<ReservationResponse>(reservation);
        }
    }
}

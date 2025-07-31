using ErrorOr;
using MapsterMapper;
using MediatR;
using Rento.Application.Common.Interfaces.Persistence;
using Rento.Application.Reservations.Common;

namespace Rento.Application.Reservations.Queries.GetByUserId
{
    public class GetReservationsByUserIdQueryHandler
    : IRequestHandler<GetReservationsByUserIdQuery, ErrorOr<List<ReservationResponse>>>
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IMapper _mapper;

        public GetReservationsByUserIdQueryHandler(IReservationRepository reservationRepository, IMapper mapper)
        {
            _reservationRepository = reservationRepository;
            _mapper = mapper;
        }

        public async Task<ErrorOr<List<ReservationResponse>>> Handle(GetReservationsByUserIdQuery request, CancellationToken cancellationToken)
        {
            var reservations = await _reservationRepository.GetByUserIdAsync(request.UserId, cancellationToken);

            return _mapper.Map<List<ReservationResponse>>(reservations);
        }
    }
}

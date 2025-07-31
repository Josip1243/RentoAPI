using Mapster;
using Rento.Application.Reservations.Common;
using Rento.Domain.Entities;

namespace Rento.Application.Common.Mapping
{
    public class ReservationMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Reservation, ReservationResponse>()
                .Map(dest => dest.Status, src => src.Status.ToString());
            config.NewConfig<Reservation, ReservationDateRangeResponse>()
               .Map(dest => dest.StartDate, src => src.StartDate)
               .Map(dest => dest.EndDate, src => src.EndDate);
        }
    }
}

using Mapster;
using Rento.Application.Vehicles.Commands.UpdateVehicle;
using Rento.Application.Vehicles.Common;
using Rento.Domain.Entities;

namespace Rento.Application.Common.Mapping
{
    public class VehicleMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Vehicle, VehicleResult>()
                .Map(dest => dest.ImageUrls, src => src.Images.Select(img => img.Url).ToList());
        }
    }
}

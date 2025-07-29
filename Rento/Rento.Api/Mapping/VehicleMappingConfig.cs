using Mapster;
using Rento.Application.Vehicles.Commands.CreateVehicle;
using Rento.Application.Vehicles.Commands.UpdateVehicle;
using Rento.Contracts.Vehicles;

namespace Rento.Api.Mapping
{
    public class VehicleMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Application.Vehicles.Common.VehicleResponse, Contracts.Vehicles.VehicleResponse>();
            config.NewConfig<CreateVehicleRequest, CreateVehicleCommand>();
            config.NewConfig<UpdateVehicleRequest, UpdateVehicleCommand>();
        }
    }
}

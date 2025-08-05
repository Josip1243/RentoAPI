using Mapster;
using Rento.Application.Vehicles.Commands.CreateVehicle;
using Rento.Application.Vehicles.Commands.UpdateVehicle;
using Rento.Application.Vehicles.Common;
using Rento.Application.Vehicles.Queries.GetAllOwnerVehicles;
using Rento.Application.Vehicles.Queries.GetAllVehiclesFilter;
using Rento.Contracts.Vehicles;

namespace Rento.Api.Mapping
{
    public class VehicleMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<VehicleResult, VehicleResponse>();
            config.NewConfig<FilteredVehicleResult, VehicleListResponse>();

            config.NewConfig<CreateVehicleRequest, CreateVehicleCommand>();
            config.NewConfig<UpdateVehicleRequest, UpdateVehicleCommand>();
            config.NewConfig<VehicleFilterRequest, GetAllVehiclesFilterQuery>();

            config.NewConfig<VehicleFilterRequest, GetAllOwnerVehiclesQuery>()
                .MapToConstructor(true)
                .Map(dest => dest.OwnerId, _ => default(int));

                    }
    }
}

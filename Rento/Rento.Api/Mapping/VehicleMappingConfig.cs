using Mapster;
using Rento.Application.Vehicles.Commands.CreateVehicle;
using Rento.Application.Vehicles.Commands.UpdateVehicle;
using Rento.Application.Vehicles.Common;
using Rento.Application.Vehicles.Queries.GetAllOwnerVehicles;
using Rento.Application.Vehicles.Queries.GetAllVehiclesFilter;
using Rento.Application.Vehicles.Queries.GetFavoriteVehicles;
using Rento.Contracts.Vehicles;

namespace Rento.Api.Mapping
{
    public class VehicleMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<VehicleResult, VehicleResponse>();
            config.NewConfig<FilteredVehicleResult, VehicleListResponse>();

            config.NewConfig<CreateVehicleRequest, CreateVehicleCommand>().MapToConstructor(true);
            config.NewConfig<UpdateVehicleRequest, UpdateVehicleCommand>().MapToConstructor(true);
            config.NewConfig<VehicleFilterRequest, GetAllVehiclesFilterQuery>();

            config.NewConfig<VehicleFilterRequest, GetAllOwnerVehiclesQuery>()
                .MapToConstructor(true)
                .Map(dest => dest.OwnerId, _ => default(int));

            config.NewConfig<Rento.Contracts.Vehicles.ImageOrderDto, Rento.Application.Vehicles.Commands.UpdateVehicleImageOrder.ImageOrderDto>();

            config.NewConfig<VehicleFilterRequest, GetFavoriteVehiclesQuery>()
                .MapToConstructor(true)
                .Map(dest => dest.UserId, _ => default(int));
        }
    }
}

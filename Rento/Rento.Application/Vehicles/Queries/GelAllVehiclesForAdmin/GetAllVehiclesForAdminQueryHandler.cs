using MediatR;
using Rento.Application.Common.Interfaces.Persistence;
using Rento.Application.Vehicles.Common;

namespace Rento.Application.Vehicles.Queries.GelAllVehiclesForAdmin
{
    public class GetAllVehiclesForAdminQueryHandler : IRequestHandler<GetAllVehiclesForAdminQuery, List<AdminVehicleResponse>>
    {
        private readonly IVehicleRepository _vehicleRepository;

        public GetAllVehiclesForAdminQueryHandler(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public async Task<List<AdminVehicleResponse>> Handle(GetAllVehiclesForAdminQuery request, CancellationToken cancellationToken)
        {
            var vehicles = await _vehicleRepository.GetAllWithOwnerAsync();

            return vehicles.Select(v => new AdminVehicleResponse
            {
                Id = v.Id,
                Brand = v.Brand,
                Model = v.Model,
                Year = v.Year,
                RegistrationNumber = v.RegistrationNumber,
                ChassisNumber = v.ChassisNumber,
                Price = v.Price,
                OwnerId = v.OwnerId,
                OwnerEmail = v.Owner.Email,
                OwnerName = $"{v.Owner.FirstName} {v.Owner.LastName}"
            }).ToList();
        }
    }

}

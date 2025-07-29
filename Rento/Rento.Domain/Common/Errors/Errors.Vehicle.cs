using ErrorOr;

namespace Rento.Domain.Common.Errors
{
    public static partial class Errors
    {
        public static class Vehicle
        {
            public static Error VehicleNotFound => Error.NotFound(
                code: "Vehicle.VehicleNotFound",
                description: "Vehicle is not found.");
        }
    }
}

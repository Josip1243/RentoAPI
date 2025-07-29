using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rento.Application.Vehicles.Queries.GetAllVehicles;

namespace Rento.Api.Controllers
{
    [Route("[controller]")]
    public class VehicleController : ApiController
    {
        private readonly ISender _mediator;

        public VehicleController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var query = new GetAllVehiclesQuery();
            var result = await _mediator.Send(query, cancellationToken);

            return result.Match(
                vehicles => Ok(vehicles),
                errors => Problem(errors)
            );
        }

        //[HttpGet("{id:int}")]
        //public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        //{
        //    var query = new GetVehicleByIdQuery(id);
        //    var result = await _mediator.Send(query, cancellationToken);

        //    return result.Match(
        //        vehicle => Ok(vehicle),
        //        errors => NotFound(errors.First().Description)
        //    );
        //}
    }
}

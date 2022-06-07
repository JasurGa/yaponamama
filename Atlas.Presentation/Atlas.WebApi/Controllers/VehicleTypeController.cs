using Atlas.Application.CQRS.VehicleTypes.Queries.GetVehicleTypeList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Atlas.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("/api/{version:apiVersion}/[controller]")]
    public class VehicleTypeController : BaseController
    {
        /// <summary>
        /// Gets the list of types of vehicles
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /api/1.0/vehicletype
        /// </remarks>
        /// <returns>Returns VehicleTypeListVm object</returns>
        /// <response code="200">Success</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<VehicleTypeListVm>> GetAllAsync()
        {
            var vm = await Mediator.Send(new GetVehicleTypeListQuery());
            return Ok(vm);
        }
    }
}

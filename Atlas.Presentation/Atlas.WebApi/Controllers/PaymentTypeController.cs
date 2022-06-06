using System;
using System.Threading.Tasks;
using Atlas.Application.CQRS.PaymentTypes.Queries.GetPaymentTypeList;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Atlas.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [Produces("applicationm/json")]
    [Route("/api/{version:apiVersion}/[controller]")]
    public class PaymentTypeController : BaseController
    {
        /// <summary>
        /// Gets payment types
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /api/1.0/paymenttype
        /// </remarks>
        /// <returns>Returns PaymentTypeListVm</returns>
        /// <response code="200">Success</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PaymentTypeListVm>> GetAllAsync()
        {
            var vm = Mediator.Send(new GetPaymentTypeListQuery { });
            return Ok(vm);
        }
    }
}

using System;
using System.Threading.Tasks;
using Atlas.Application.CQRS.NotificationTypes.Queries.GetNotificationTypes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Atlas.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("/api/{version:apiVersion}/[controller]")]
    public class NotificationTypeController : BaseController
    {
        /// <summary>
        /// Gets the list of notification types
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     GET /api/1.0/notificationtype
        ///     
        /// </remarks>
        /// <returns>Returns NotificationTypeListVm object</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<NotificationTypeListVm>> GetAllAsync()
        {
            var vm = await Mediator.Send(new GetNotificationTypesQuery());
            return Ok(vm);
        }
    }
}

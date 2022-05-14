using System;
using System.Threading.Tasks;
using Atlas.Application.CQRS.Notifications.Queries.GetNotificationDetails;
using Atlas.Application.CQRS.Notifications.Queries.GetNotificationsPagedList;
using Atlas.Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Atlas.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("/api/{version:apiVersion}/[controller]")]
    public class NotificationController : BaseController
    {
        /// <summary>
        /// Get the notification details
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /api/1.0/notification/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        /// </remarks>
        /// <param name="id">Notification id (guid)</param>
        /// <returns>Returns NotificationDetailsVm object</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not found</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<NotificationDetailsVm>> GetByIdAsync([FromRoute] Guid id)
        {
            var vm = await Mediator.Send(new GetNotificationDetailsQuery
            {
                Id     = id,
                UserId = UserId
            });

            return Ok(vm);
        }

        /// <summary>
        /// Get the list of notifications
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /api/1.0/notification/paged?pageIndex=0&amp;pageSize=10
        /// </remarks>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>Returns PageDto NotificationLookupDto object</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpGet("paged")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<PageDto<NotificationLookupDto>>> GetPagedAsync([FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 10)
        {
            var vm = await Mediator.Send(new GetNotificationsPagedListQuery
            {
                UserId    = UserId,
                PageIndex = pageIndex,
                PageSize  = pageSize
            });

            return Ok(vm);
        }
    }
}

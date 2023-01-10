using System;
using System.Threading.Tasks;
using Atlas.Application.Common.Constants;
using Atlas.Application.CQRS.PushNotifications.Commands.CreatePushNotification;
using Atlas.Application.CQRS.PushNotifications.Commands.DeletePushNotification;
using Atlas.Application.CQRS.PushNotifications.Commands.UpdatePushNotification;
using Atlas.Application.CQRS.PushNotifications.Queries.GetPushNotificationById;
using Atlas.Application.CQRS.PushNotifications.Queries.GetPushNotificationsPagedList;
using Atlas.WebApi.Filters;
using Atlas.WebApi.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Atlas.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("/api/{version:apiVersion}/[controller]")]
    public class PushNotificationController : BaseController
    {
        private readonly IMapper _mapper;

        public PushNotificationController(IMapper mapper) =>
            _mapper = mapper;

        /// <summary>
        /// Creates a push notification
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/1.0/pushnotification
        ///     {
        ///         "headerRu": "Lorem ipsum",
        ///         "headerEn": "Lorem ipsum",
        ///         "headerUz": "Lorem ipsum",
        ///         "bodyRu": "Lorem ipsum",
        ///         "bodyEn": "Lorem ipsum",
        ///         "bodyUz": "Lorem ipsum",
        ///         "expiresAt": "1900-01-01T01:01:01"
        ///     }
        ///     
        /// </remarks>
        /// <returns>Returns id (Guid)</returns>
        /// <param name="createPushNotificationDto">CreatePushNotificationDto object</param>
        /// <response code="200">Success</response>
        /// <response code="404">NotFound</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPost]
        [Authorize]
        [AuthRoleFilter(Roles.Admin)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Guid>> CreateAsync([FromBody] CreatePushNotificationDto createPushNotificationDto)
        {
            var vm = await Mediator.Send(_mapper.Map<CreatePushNotificationDto,
                CreatePushNotificationCommand>(createPushNotificationDto));

            return Ok(vm);
        }

        /// <summary>
        /// Updates a push notification
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     PUT /api/1.0/pushnotification
        ///     {
        ///         "id": "a3eb7b4a-9f4e-4c71-8619-398655c563b8",
        ///         "headerRu": "Lorem ipsum",
        ///         "headerEn": "Lorem ipsum",
        ///         "headerUz": "Lorem ipsum",
        ///         "bodyRu": "Lorem ipsum",
        ///         "bodyEn": "Lorem ipsum",
        ///         "bodyUz": "Lorem ipsum",
        ///         "expiresAt": "1900-01-01T01:01:01"
        ///     }
        ///     
        /// </remarks>
        /// <returns>Returns NoContent</returns>
        /// <param name="updatePushNotificationDto">UpdatePushNotificationDto object</param>
        /// <response code="204">Success</response>
        /// <response code="404">NotFound</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPut]
        [Authorize]
        [AuthRoleFilter(Roles.Admin)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> UpdateAsync([FromBody] UpdatePushNotificationDto updatePushNotificationDto)
        {
            var vm = await Mediator.Send(_mapper.Map<UpdatePushNotificationDto,
                UpdatePushNotificationCommand>(updatePushNotificationDto));

            return NoContent();
        }

        /// <summary>
        /// Deletes a push notification
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     DELETE /api/1.0/pushnotification/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        ///     
        /// </remarks>
        /// <returns>Returns NoContent</returns>
        /// <param name="id">PushNotification Id (Guid)</param>
        /// <response code="204">Success</response>
        /// <response code="404">NotFound</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpDelete("{id}")]
        [AuthRoleFilter(Roles.Admin)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> DeleteAsync([FromRoute] Guid id)
        {
            var vm = await Mediator.Send(new DeletePushNotificationCommand
            {
                Id = id
            });

            return NoContent();
        }

        /// <summary>
        /// Get a push notification by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     GET /api/1.0/pushnotification/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        ///     
        /// </remarks>
        /// <returns>Returns PushNotificationLookupDto</returns>
        /// <param name="id">PushNotification Id (Guid)</param>
        /// <response code="200">Success</response>
        /// <response code="404">NotFound</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PushNotificationLookupDto>> GetByIdAsync([FromRoute] Guid id)
        {
            var vm = await Mediator.Send(new GetPushNotificationByIdQuery
            {
                Id = id
            });

            return Ok(vm);
        }

        /// <summary>
        /// Get paged list of push notifications
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     GET /api/1.0/pushnotification/paged?pageSize=10&amp;pageIndex=0
        ///     
        /// </remarks>
        /// <returns>Returns PageDto PushNotificationLookupDto object</returns>
        /// <param name="pageSize">Page size (int)</param>
        /// <param name="pageIndex">Page index (int)</param>
        /// <response code="200">Success</response>
        /// <response code="404">NotFound</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpGet("paged")]
        [AuthRoleFilter(Roles.Admin)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<PushNotificationLookupDto>> GetPagedAsync([FromQuery] int pageSize = 10,
            [FromQuery] int pageIndex = 0)
        {
            var vm = await Mediator.Send(new GetPushNotificationsPagedListQuery
            {
                PageSize  = pageSize,
                PageIndex = pageIndex
            });

            return Ok(vm);
        }
    }
}


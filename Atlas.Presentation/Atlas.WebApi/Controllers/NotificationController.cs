using System;
using System.Threading.Tasks;
using Atlas.Application.Common.Constants;
using Atlas.Application.CQRS.Notifications.Commands.AttachNotificationToRole;
using Atlas.Application.CQRS.Notifications.Commands.AttachNotificationToUser;
using Atlas.Application.CQRS.Notifications.Commands.CreateNotification;
using Atlas.Application.CQRS.Notifications.Commands.DeleteNotification;
using Atlas.Application.CQRS.Notifications.Commands.DettachNotificationToRole;
using Atlas.Application.CQRS.Notifications.Commands.DettachNotificationToUser;
using Atlas.Application.CQRS.Notifications.Commands.UpdateNotification;
using Atlas.Application.CQRS.Notifications.Queries.GetNotificationDetails;
using Atlas.Application.CQRS.Notifications.Queries.GetNotificationsPagedList;
using Atlas.Application.Models;
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
    public class NotificationController : BaseController
    {
        private readonly IMapper _mapper;

        public NotificationController(IMapper mapper) =>
            _mapper = mapper;

        /// <summary>
        /// Attaches the notification to user
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     POST /api/1.0/notification/a3eb7b4a-9f4e-4c71-8619-398655c563b8/user/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        ///     
        /// </remarks>
        /// <param name="notificationId">Notification id (guid)</param>
        /// <param name="userId">User id (guid)</param>
        /// <returns>Returns Guid</returns>
        /// <response code="204">Success</response>
        /// <response code="404">NotFound</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpPost("{notificationId}/user/{userId}")]
        [AuthRoleFilter(new string[] { Roles.Admin, Roles.Support })]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> AttachesAsync([FromRoute] Guid notificationId,
            [FromRoute] Guid userId)
        {
            await Mediator.Send(new AttachNotificationToUserCommand
            {
                UserId         = userId,
                NotificationId = notificationId
            });

            return NoContent();
        }

        /// <summary>
        /// Dettaches the notification to user
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     DELETE /api/1.0/notification/a3eb7b4a-9f4e-4c71-8619-398655c563b8/user/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        ///     
        /// </remarks>
        /// <param name="notificationId">Notification id (guid)</param>
        /// <param name="userId">User id (guid)</param>
        /// <returns>Returns Guid</returns>
        /// <response code="204">Success</response>
        /// <response code="404">NotFound</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpDelete("{notificationId}/user/{userId}")]
        [AuthRoleFilter(new string[] { Roles.Admin, Roles.Support })]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> DettachesAsync([FromRoute] Guid notificationId,
            [FromRoute] Guid userId)
        {
            await Mediator.Send(new DettachNotificationToUserCommand
            {
                UserId         = userId,
                NotificationId = notificationId
            });

            return NoContent();
        }

        /// <summary>
        /// Attaches the notification to role
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     POST /api/1.0/notification/a3eb7b4a-9f4e-4c71-8619-398655c563b8/role/Admin
        ///     
        /// </remarks>
        /// <param name="notificationId">Notification id (guid)</param>
        /// <param name="role">Role (string)</param>
        /// <returns>Returns Guid</returns>
        /// <response code="204">Success</response>
        /// <response code="404">NotFound</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpPost("{notificationId}/role/{role}")]
        [AuthRoleFilter(new string[] { Roles.Admin, Roles.Support })]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> AttachesAsync([FromRoute] Guid notificationId,
            [FromRoute] string role)
        {
            await Mediator.Send(new AttachNotificationToRoleCommand
            {
                Role           = role,
                NotificationId = notificationId
            });

            return NoContent();
        }

        /// <summary>
        /// Dettaches the notification from role
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     DELETE /api/1.0/notification/a3eb7b4a-9f4e-4c71-8619-398655c563b8/role/Admin
        ///     
        /// </remarks>
        /// <param name="notificationId">Notification id (guid)</param>
        /// <param name="role">Role (string)</param>
        /// <returns>Returns Guid</returns>
        /// <response code="204">Success</response>
        /// <response code="404">NotFound</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpDelete("{notificationId}/role/{role}")]
        [AuthRoleFilter(new string[] { Roles.Admin, Roles.Support })]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> DettachesAsync([FromRoute] Guid notificationId,
            [FromRoute] string role)
        {
            await Mediator.Send(new DettachNotificationToRoleCommand
            {
                Role           = role,
                NotificationId = notificationId
            });

            return NoContent();
        }

        /// <summary>
        /// Creates the notification
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     POST /api/1.0/notification
        ///     {
        ///         "notificationTypeId": "a3eb7b4a-9f4e-4c71-8619-398655c563b8",
        ///         "subject": "Sample subject",
        ///         "subjectRu": "Sample subject",
        ///         "subjectEn": "Sample subject",
        ///         "subjectUz": "Sample subject",
        ///         "body": "Sample body",
        ///         "bodyRu": "Sample body",
        ///         "bodyEn": "Sample body",
        ///         "bodyUz": "Sample body",
        ///         "priority": 0
        ///     }
        ///     
        /// </remarks>
        /// <param name="createNotificationDto">CreateNotificationDto object</param>
        /// <returns>Returns Guid</returns>
        /// <response code="200">Success</response>
        /// <response code="404">NotFound</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPost]
        [Authorize]
        [AuthRoleFilter(new string[] { Roles.Admin, Roles.Support })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Guid>> CreateAsync([FromBody] CreateNotificationDto createNotificationDto)
        {
            var notificationId = await Mediator.Send(_mapper.Map<CreateNotificationDto,
                CreateNotificationCommand>(createNotificationDto));

            return Ok(notificationId);
        }

        /// <summary>
        /// Updates the notification
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     PUT /api/1.0/notification
        ///     {
        ///         "id": "a3eb7b4a-9f4e-4c71-8619-398655c563b8",
        ///         "notificationTypeId": "a3eb7b4a-9f4e-4c71-8619-398655c563b8",
        ///         "subject": "Sample subject",
        ///         "subjectRu": "Sample subject",
        ///         "subjectEn": "Sample subject",
        ///         "subjectUz": "Sample subject",
        ///         "body": "Sample body",
        ///         "bodyRu": "Sample body",
        ///         "bodyEn": "Sample body",
        ///         "bodyUz": "Sample body",
        ///         "priority": 0
        ///     }
        ///     
        /// </remarks>
        /// <param name="updateNotificationDto">UpdateNotificationDto object</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="404">NotFound</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPut]
        [Authorize]
        [AuthRoleFilter(new string[] { Roles.Admin, Roles.Support })]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> UpdateAsync([FromBody] UpdateNotificationDto updateNotificationDto)
        {
            await Mediator.Send(_mapper.Map<UpdateNotificationDto,
                UpdateNotificationCommand>(updateNotificationDto));

            return NoContent();
        }

        /// <summary>
        /// Deletes the notification
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     DELTE /api/1.0/notification/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        ///     
        /// </remarks>
        /// <param name="id">Notification id (guid)</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="404">NotFound</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpDelete("{id}")]
        [AuthRoleFilter(new string[] { Roles.Admin, Roles.Support })]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> DeleteAsync([FromRoute] Guid id)
        {
            await Mediator.Send(new DeleteNotificationCommand
            {
                Id = id
            });

            return NoContent();
        }

        /// <summary>
        /// Get the notification details
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     GET /api/1.0/notification/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        ///     
        /// </remarks>
        /// <param name="id">Notification id (guid)</param>
        /// <returns>Returns NotificationDetailsVm object</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not found</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpGet("{id}")]
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
        ///     
        ///     GET /api/1.0/notification/paged?pageIndex=0&amp;pageSize=10
        ///     
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
        public async Task<ActionResult<PageDto<NotificationLookupDto>>> GetPagedAsync(
            [FromQuery] int pageIndex = 0,
            [FromQuery] int pageSize  = 10)
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

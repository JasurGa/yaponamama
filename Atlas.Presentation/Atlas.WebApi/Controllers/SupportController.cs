﻿using Atlas.Application.Common.Constants;
using Atlas.Application.CQRS.Supports.Commands.CreateSupport;
using Atlas.Application.CQRS.Supports.Commands.DeleteSupport;
using Atlas.Application.CQRS.Supports.Commands.RestoreSupport;
using Atlas.Application.CQRS.Supports.Commands.UpdateSupport;
using Atlas.Application.CQRS.Supports.Queries.FindSupportsPagedList;
using Atlas.Application.CQRS.Supports.Queries.GetSupportDetails;
using Atlas.Application.CQRS.Supports.Queries.GetSupportPagedList;
using Atlas.Application.CQRS.Users.Commands.CreateUser;
using Atlas.Application.Models;
using Atlas.WebApi.Filters;
using Atlas.WebApi.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Atlas.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("/api/{version:apiVersion}/[controller]")]
    public class SupportController : BaseController
    {
        private readonly IMapper _mapper;

        public SupportController(IMapper mapper) =>
            _mapper = mapper;

        /// <summary>
        /// Search admins
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/1.0/support/search?searchQuery=bla+bla+bla&amp;pageIndex=0&amp;pageSize=0&amp;showDeleted=false
        ///     
        /// </remarks>
        /// <param name="searchQuery">Search Query (string)</param>
        /// <param name="pageSize">Page Size (int)</param>
        /// <param name="pageIndex">Page Index (int)</param>
        /// <param name="showDeleted">Show deleted (bool)</param>
        /// <returns>Returns PageDto ClientLookupDto</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not Found</response>
        [Authorize]
        [HttpGet("search")]
        [AuthRoleFilter(new string[] { Roles.Admin, Roles.HeadRecruiter, Roles.SupplyManager, Roles.Support })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PageDto<SupportLookupDto>>> SearchAsync([FromQuery] string searchQuery,
            [FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 10, [FromQuery] bool showDeleted = false)
        {
            var vm = await Mediator.Send(new FindSupportsPagedListQuery
            {
                ShowDeleted = showDeleted,
                SearchQuery = searchQuery,
                PageSize    = pageSize,
                PageIndex   = pageIndex
            });

            return Ok(vm);
        }

        /// <summary>
        /// Gets the paged list of supports
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     GET /api/1.0/support/paged?showDeleted=false&amp;pageIndex=0&amp;pageSize=10
        ///     
        /// </remarks>
        /// <param name="showDeleted">Show deleted</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>Returns PageDto SupportLookupDto object</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpGet("paged")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<PageDto<SupportLookupDto>>> GetAllPagedAsync(
            [FromQuery] bool showDeleted = false, 
            [FromQuery] int pageIndex = 0, 
            [FromQuery] int pageSize = 10)
        {
            var vm = await Mediator.Send(new GetSupportPagedListQuery
            {
                ShowDeleted = showDeleted,
                PageIndex = pageIndex,
                PageSize = pageSize
            });

            return Ok(vm);
        }

        /// <summary>
        /// Gets the support details
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     GET /api/1.0/support/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        ///     
        /// </remarks>
        /// <param name="id">Support id (guid)</param>
        /// <returns>Returns SupportDetailsVm object</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not found</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<SupportDetailsVm>> GetByIdAsync([FromRoute] Guid id)
        {
            var vm = await Mediator.Send(new GetSupportDetailsQuery
            {
                Id = id,
            });

            return Ok(vm);
        }

        /// <summary>
        /// Creates a new support
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     POST /api/1.0/support
        ///     {
        ///         "user": {
        ///             "login": "admin",
        ///             "password": "admin",
        ///             "firstName": "Ivan",
        ///             "lastName": "Ivan",
        ///             "middleName": "Ivanovich",
        ///             "sex": 0,
        ///             "birthday": "1990-01-01T10:00:00",
        ///             "avatarPhotoPath": "/0123456789abcdef0123456789abcdef.png"
        ///         },
        ///         "internalPhoneNumber": "+998901234567"
        ///         "passportPhotoPath": "/storage/passportPhotos/ppp986.jpg",
        ///         "salary": 1000000,
        ///         "startOfWorkingHours": "01-01-1901T10:00:00",
        ///         "workingDayDuration": 8,
        ///     }
        ///     
        /// </remarks>
        /// <param name="createSupport">CreateSupportDto object</param>
        /// <returns>Returns id (guid)</returns> 
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="401">Not found</response>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Guid>> CreateAsync([FromBody] CreateSupportDto createSupport)
        {
            var supportId = await Mediator.Send(_mapper.Map<CreateSupportDto,
                CreateSupportCommand>(createSupport));

            return Ok(supportId);
        }

        /// <summary>
        /// Updates the support
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     PUT /api/1.0/support
        ///     {
        ///         "id": "a3eb7b4a-9f4e-4c71-8619-398655c563b8",
        ///         "user": {
        ///             "id": "a3eb7b4a-9f4e-4c71-8619-398655c563b8",
        ///             "login": "admin",
        ///             "password": "admin",
        ///             "firstName": "Ivan",
        ///             "lastName": "Ivan",
        ///             "middleName": "Ivanovich",
        ///             "sex": 0,
        ///             "birthday": "1990-01-01T10:00:00",
        ///             "avatarPhotoPath": "/0123456789abcdef0123456789abcdef.png"
        ///         },
        ///         "internalPhoneNumber": "+998901234567"
        ///         "passportPhotoPath": "/storage/passportPhotos/ppp986.jpg",
        ///         "salary": 1000000,
        ///         "startOfWorkingHours": "01-01-1901T10:00:00",
        ///         "workingDayDuration": 8,
        ///     }
        ///     
        /// </remarks>
        /// <param name="updateSupport">UpdateSupportDto object</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="404">Not found</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPut]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> UpdateAsync([FromBody] UpdateSupportDto updateSupport)
        {
            await Mediator.Send(_mapper.Map<UpdateSupportDto,
                UpdateSupportCommand>(updateSupport));

            return NoContent();
        }

        /// <summary>
        /// Deletes the support
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     DELETE /api/1.0/support/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        ///     
        /// </remarks>
        /// <param name="id">Support id (guid)</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="404">Not found</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            await Mediator.Send(new DeleteSupportCommand
            {
                Id = id,
            });

            return NoContent();
        }

        /// <summary>
        /// Restore the support
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     PATCH /api/1.0/support/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        ///     
        /// </remarks>
        /// <param name="id">Support id (guid)</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="404">Not found</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> RestoreAsync([FromRoute] Guid id)
        {
            await Mediator.Send(new RestoreSupportCommand
            {
                Id = id,
            });

            return NoContent();
        }
    }
}

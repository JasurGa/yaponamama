using Atlas.Application.Common.Constants;
using Atlas.Application.CQRS.Users.Commands.DeleteUser;
using Atlas.Application.CQRS.Users.Commands.RestoreUser;
using Atlas.Application.CQRS.Users.Commands.UpdateUser;
using Atlas.Application.CQRS.Users.Queries.GetUserDetails;
using Atlas.Application.CQRS.Users.Queries.GetUserPagedList;
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
    public class UserController : BaseController
    {
        private readonly IMapper _mapper;

        public UserController(IMapper mapper) =>
            _mapper = mapper;

        /// <summary>
        /// Get the paged list of users
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /api/1.0/user/paged?showDeleted=false&amp;pageIndex=0&amp;pageSize=10
        /// </remarks>
        /// <param name="showDeleted">Show deleted list</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>Returns PageDto UserLookupDto object</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpGet("paged")]
        [AuthRoleFilter(new string[] { Roles.Admin, Roles.Support })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<PageDto<UserLookupDto>>> GetAllPagedAsync([FromQuery] bool showDeleted = false, [FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 10)
        {
            var vm = await Mediator.Send(new GetUserPagedListQuery
            {
                ShowDeleted = showDeleted,
                PageIndex   = pageIndex,
                PageSize    = pageSize
            });

            return Ok(vm);
        }

        /// <summary>
        /// Gets the user by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /api/1.0/user/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        /// </remarks>
        /// <param name="id">User id (guid)</param>
        /// <returns>Returns UserDetailsVm object</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not found</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpGet("{id}")]
        [AuthRoleFilter(new string[] { Roles.Admin, Roles.Support })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<UserDetailsVm>> GetAsync(Guid id)
        {
            var vm = await Mediator.Send(new GetUserDetailsQuery
            {
                Id = id,
            });

            return Ok(vm);
        }

        /// <summary>
        /// Updates the user
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// PUT /api/1.0/user
        /// {
        ///     "id": "a3eb7b4a-9f4e-4c71-8619-398655c563b8",
        ///     "firstName": "John",
        ///     "lastName": "Doe",
        ///     "birthday": "1900-01-01T10:00:00",
        ///     "avatarPhotoPath": "/main/dir,
        /// }
        /// </remarks>
        /// <param name="updateUser">UpdateUserDto object</param>
        /// <returns>Returns id (guid)</returns> 
        /// <response code="204">Success</response>
        /// <response code="404">Not found</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPut]
        [Authorize]
        [AuthRoleFilter(new string[] { Roles.Admin, Roles.Support })]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Guid>> UpdateAsync([FromBody] UpdateUserDto updateUser)
        {
            await Mediator.Send(_mapper.Map<UpdateUserDto,
                UpdateUserCommand>(updateUser));

            return NoContent();
        }

        /// <summary>
        /// Deletes the user by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE /api/1.0/user/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        /// </remarks>
        /// <param name="id">User id</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="404">Not found</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpDelete("{id}")]
        [AuthRoleFilter(new string[] { Roles.Admin, Roles.Support })]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            await Mediator.Send(new DeleteUserCommand
            {
                Id = id,
            });

            return NoContent();
        }

        /// <summary>
        /// Restores the user by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// PATCH /api/1.0/user/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        /// </remarks>
        /// <param name="id">User id</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="404">Not found</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpPatch("{id}")]
        [AuthRoleFilter(new string[] { Roles.Admin, Roles.Support })]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> RestoreAsync(Guid id)
        {
            await Mediator.Send(new RestoreUserCommand
            {
                Id = id,
            });

            return NoContent();
        }
    }
}

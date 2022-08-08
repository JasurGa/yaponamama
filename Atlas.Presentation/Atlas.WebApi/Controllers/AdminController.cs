using System;
using System.Threading.Tasks;
using Atlas.Application.Common.Constants;
using Atlas.Application.CQRS.Admins.Commands.CreateAdmin;
using Atlas.Application.CQRS.Admins.Commands.DeleteAdmin;
using Atlas.Application.CQRS.Admins.Commands.RestoreAdmin;
using Atlas.Application.CQRS.Admins.Commands.UpdateAdmin;
using Atlas.Application.CQRS.Admins.Queries.GetAdminDetails;
using Atlas.Application.CQRS.Admins.Queries.GetAdminPagedList;
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
    public class AdminController : BaseController
    {
        private readonly IMapper _mapper;

        public AdminController(IMapper mapper) =>
            _mapper = mapper;

        /// <summary>
        /// Creates a new admin
        /// </summary>
        /// <remarks>
        /// POST /api/1.0/admin
        /// {
        ///     "phoneNumber": "+998901234567",
        ///     "startOfWorkingHours": "01-01-1901T10:00:00",
        ///     "workingDayDuration: 8,
        ///     "salary": 10000000,
        ///     "officialRoleId": "a3eb7b4a-9f4e-4c71-8619-398655c563b8",
        ///     "userId": "a3eb7b4a-9f4e-4c71-8619-398655c563b8"
        /// }
        /// </remarks>
        /// <param name="createAdmin">CreateAdminDto object</param>
        /// <returns>Returns id (Guid)</returns>
        /// <response code="200">Success</response>
        /// <response code="404">NotFound</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPost]
        [Authorize]
        [AuthRoleFilter(Roles.Admin)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Guid>> CreateAsync([FromBody] CreateAdminDto createAdmin)
        {
            var vm = await Mediator.Send(_mapper.Map<CreateAdminDto,
                CreateAdminCommand>(createAdmin));

            return Ok(vm);
        }

        /// <summary>
        /// Gets the admin details
        /// </summary>
        /// <remarks>
        /// GET /api/1.0/admin/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        /// </remarks>
        /// <param name="id">Admin id (guid)</param>
        /// <returns>Returns AdminDetailsVm</returns>
        /// <response code="200">Success</response>
        /// <response code="404">NotFound</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpGet("{id}")]
        [AuthRoleFilter(Roles.Admin)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<AdminDetailsVm>> GetByIdAsync([FromRoute] Guid id)
        {
            var vm = await Mediator.Send(new GetAdminDetailsQuery
            {
                Id = id
            });

            return Ok(vm);
        }

        /// <summary>
        /// Gets the list of admins
        /// </summary>
        /// <remarks>
        /// GET /api/1.0/admin/paged?showDeleted=true&pageSize=10&pageIndex=0
        /// </remarks>
        /// <param name="showDeleted">ShowDeleted (bool)</param>
        /// <param name="pageSize">Size of the page</param>
        /// <param name="pageIndex">Index of the page</param>
        /// <param name="sortable">Property to sort</param>
        /// <param name="ascending">Ascending (true) || Descending (false)</param>
        /// <returns>PageDto AdminLookupDto object</returns>
        /// <response code="200">Success</response>
        /// <response code="404">NotFound</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpGet("paged")]
        [AuthRoleFilter(Roles.Admin)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<PageDto<AdminLookupDto>>> GetAllAsync(
            [FromQuery] bool showDeleted = false,
            [FromQuery] int pageSize = 10, 
            [FromQuery] int pageIndex = 0,
            [FromQuery] string sortable = "Name",
            [FromQuery] bool ascending = true)
        {
            var vm = await Mediator.Send(new GetAdminPagedListQuery
            {
                PageSize    = pageSize,
                PageIndex   = pageIndex,
                ShowDeleted = showDeleted,
                Sortable    = sortable,
                Ascending   = ascending
            });

            return Ok(vm);
        }

        /// <summary>
        /// Updates the admin
        /// </summary>
        /// <remarks>
        /// PUT /api/1.0/admin
        /// {
        ///     "id": "a3eb7b4a-9f4e-4c71-8619-398655c563b8",
        ///     "phoneNumber": "+998901234567",
        ///     "startOfWorkingHours": "01-01-1901T10:00:00",
        ///     "workingDayDuration: 8,
        ///     "salary": 1000000,
        ///     "officialRoleId": "a3eb7b4a-9f4e-4c71-8619-398655c563b8",
        ///     "userId": "a3eb7b4a-9f4e-4c71-8619-398655c563b8"
        /// }
        /// </remarks>
        /// <param name="updateAdmin">UpdateAdminDto object</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="404">NotFound</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPut]
        [Authorize]
        [AuthRoleFilter(Roles.Admin)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> UpdateAsync([FromBody] UpdateAdminDto updateAdmin)
        {
            var vm = await Mediator.Send(_mapper.Map<UpdateAdminDto,
                UpdateAdminCommand>(updateAdmin));

            return Ok(vm);
        }

        /// <summary>
        /// Deletes the admin
        /// </summary>
        /// <remarks>
        /// DELETE /api/1.0/admin/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        /// </remarks>
        /// <param name="id">Admin id (guid)</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="404">NotFound</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpDelete("{id}")]
        [AuthRoleFilter(Roles.Admin)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> DeleteAsync([FromRoute] Guid id)
        {
            var vm = await Mediator.Send(new DeleteAdminCommand
            {
                Id = id
            });

            return Ok(vm);
        }

        /// <summary>
        /// Restores the admin
        /// </summary>
        /// <remarks>
        /// PATCH /api/1.0/admin/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        /// </remarks>
        /// <param name="id">Admin id (guid)</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="404">NotFound</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpPatch("{id}")]
        [AuthRoleFilter(Roles.Admin)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> RestoreAsync([FromRoute] Guid id)
        {
            var vm = await Mediator.Send(new RestoreAdminCommand
            {
                Id = id
            });

            return Ok(vm);
        }
    }
}

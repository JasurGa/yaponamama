using Atlas.Application.Common.Constants;
using Atlas.Application.CQRS.Clients.Commands.DeleteClient;
using Atlas.Application.CQRS.Clients.Commands.RestoreClient;
using Atlas.Application.CQRS.Clients.Commands.UpdateClient;
using Atlas.Application.CQRS.Clients.Queries.FindClientPagedList;
using Atlas.Application.CQRS.Clients.Queries.GetClientDetails;
using Atlas.Application.CQRS.Clients.Queries.GetClientPagedList;
using Atlas.Application.CQRS.Clients.Queries.GetClientsList;
using Atlas.Application.CQRS.Orders.Queries.FindOrderPagedList;
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
    public class ClientController : BaseController
    {
        private readonly IMapper _mapper;

        public ClientController(IMapper mapper) =>
            _mapper = mapper;

        /// <summary>
        /// Search clients
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/1.0/client/search?searchQuery=bla+bla+bla&amp;pageIndex=0&amp;pageSize=0&amp;showDeleted=false
        ///     
        /// </remarks>
        /// <param name="searchQuery">Search Query (string)</param>
        /// <param name="pageSize">Page Size (int)</param>
        /// <param name="pageIndex">Page Index (int)</param>
        /// <returns>Returns PageDto ClientLookupDto</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not Found</response>
        [Authorize]
        [HttpGet("search")]
        [AuthRoleFilter(new string[] { Roles.Admin, Roles.HeadRecruiter, Roles.SupplyManager, Roles.Support })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PageDto<ClientLookupDto>>> SearchAsync([FromQuery] string searchQuery,
            [FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 10, [FromQuery] bool showDeleted = false)
        {
            var vm = await Mediator.Send(new FindClientPagedListQuery
            {
                SearchQuery = searchQuery,
                PageSize    = pageSize,
                PageIndex   = pageIndex,
                ShowDeleted = showDeleted
            });

            return Ok(vm);
        }

        /// <summary>
        /// Gets the paged list of clients
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     GET /api/1.0/client/paged?showDeleted=false&amp;pageIndex=0&amp;pageSize=10
        ///     
        /// </remarks>
        /// <param name="showDeleted">Show deleted</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>Returns PageDto ClientLookupDto object</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpGet("paged")]
        [AuthRoleFilter(new string[] { Roles.Admin, Roles.Support })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<PageDto<ClientLookupDto>>> GetAllPagedAsync([FromQuery] bool showDeleted = false, [FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 10)
        {
            var vm = await Mediator.Send(new GetClientPagedListQuery
            {
                ShowDeleted = showDeleted,
                PageIndex = pageIndex,
                PageSize = pageSize
            });

            return Ok(vm);
        }

        /// <summary>
        /// Gets the client details
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     GET /api/1.0/client/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        ///     
        /// </remarks>
        /// <param name="id">Client id (guid)</param>
        /// <returns>Returns ClientDetailsVm object</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not found</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpGet("{id}")]
        [AuthRoleFilter(new string[] { Roles.Admin, Roles.Support })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ClientDetailsVm>> GetByIdAsync([FromRoute] Guid id)
        {
            var vm = await Mediator.Send(new GetClientDetailsQuery
            {
                Id = id,
            });

            return Ok(vm);
        }

        /// <summary>
        /// Updates the client
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT /api/1.0/client
        ///     {
        ///         "id": "a3eb7b4a-9f4e-4c71-8619-398655c563b8",
        ///         "phoneNumber": "+998901234567",
        ///         "passportPhotoPath": "789012389329130.pdf",
        ///         "selfieWithPassportPhotoPath": "345678919583.jpg",
        ///         "isPassportVerified": true,
        ///         "user": {
        ///             "id": "a3eb7b4a-9f4e-4c71-8619-398655c563b8",
        ///             "login": "+998901234567",
        ///             "password": "password",
        ///             "firstName": "John",
        ///             "lastName": "Doe",
        ///             "middleName": "O'Neal",
        ///             "sex": 1,
        ///             "birthday": "2022-09-05T12:57:43.404Z",
        ///             "avatarPhotoPath": "56789123729131.jpg"
        ///         }
        ///     }
        ///     
        /// </remarks>
        /// <param name="updateClient">UpdateClientDto object</param>
        /// <returns>Returns id (guid)</returns> 
        /// <response code="204">Success</response>
        /// <response code="404">Not found</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPut]
        [Authorize]
        [AuthRoleFilter(new string[] { Roles.Admin, Roles.SupplyManager })]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Guid>> UpdateAsync([FromBody] UpdateClientDto updateClient)
        {
            await Mediator.Send(_mapper.Map<UpdateClientDto,
                UpdateClientCommand>(updateClient));

            return NoContent();
        }

        /// <summary>
        /// Deletes the client by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     DELETE /api/1.0/client/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        ///     
        /// </remarks>
        /// <param name="id">Client id</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="404">Not found</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpDelete("{id}")]
        [AuthRoleFilter(new string[] { Roles.Admin, Roles.SupplyManager })]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            await Mediator.Send(new DeleteClientCommand
            {
                Id = id
            });

            return NoContent();
        }

        /// <summary>
        /// Restores the client by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PATCH /api/1.0/client/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        ///     
        /// </remarks>
        /// <param name="id">Client id</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="404">Not found</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpPatch("{id}")]
        [AuthRoleFilter(new string[] { Roles.Admin, Roles.SupplyManager })]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> RestoreAsync([FromRoute] Guid id)
        {
            await Mediator.Send(new RestoreClientCommand
            {
                Id = id,
            });

            return NoContent();
        }

    }
}

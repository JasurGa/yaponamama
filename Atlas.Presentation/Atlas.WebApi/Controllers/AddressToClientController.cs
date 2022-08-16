using System;
using System.Threading.Tasks;
using Atlas.Application.Common.Constants;
using Atlas.Application.CQRS.AddressToClients.Commands.CreateAddressToClient;
using Atlas.Application.CQRS.AddressToClients.Commands.DeleteAddressToClient;
using Atlas.Application.CQRS.AddressToClients.Commands.UpdateAddressToClient;
using Atlas.Application.CQRS.AddressToClients.Queries.GetAddressToClientDetails;
using Atlas.Application.CQRS.AddressToClients.Queries.GetAddressToClientList;
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
    public class AddressToClientController : BaseController
    {
        private readonly IMapper _mapper;

        public AddressToClientController(IMapper mapper) =>
            _mapper = mapper;

        /// <summary>
        /// Creates the address
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     POST /api/1.0/addresstoclient
        ///     {
        ///         "address": "Sample address",
        ///         "latitude": 0,
        ///         "longitude": 0
        ///     }
        ///     
        /// </remarks>
        /// <param name="createAddressToClient">CreateAddressToClientDto object</param>
        /// <returns>Returns id (guid)</returns> 
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPost]
        [Authorize]
        [AuthRoleFilter(Roles.Client)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Guid>> CreateAsync([FromBody] CreateAddressToClientDto createAddressToClient)
        {
            var command = _mapper.Map<CreateAddressToClientCommand>(createAddressToClient, opt =>
                opt.AfterMap((src, dst) => dst.ClientId = ClientId));

            var addressToClientId = await Mediator.Send(command);
            return Ok(addressToClientId);
        }

        /// <summary>
        /// Gets the list of addresses
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     GET /api/1.0/addresstoclient
        ///     
        /// </remarks>
        /// <returns>Returns AddressToClientListVm object</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpGet]
        [Authorize]
        [AuthRoleFilter(Roles.Client)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<AddressToClientListVm>> GetAllAsync()
        {
            var vm = await Mediator.Send(new GetAddressToClientListQuery
            {
                ClientId = ClientId,
            });

            return Ok(vm);
        }

        /// <summary>
        /// Gets the address by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     GET /api/1.0/addresstoclient/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        ///     
        /// </remarks>
        /// <param name="id">AddressToClient id (guid)</param>
        /// <returns>Returns AddressToClientDetailsVm object</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not found</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpGet("{id}")]
        [AuthRoleFilter(Roles.Client)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<AddressToClientDetailsVm>> GetAsync(Guid id)
        {
            var vm = await Mediator.Send(new GetAddressToClientDetailsQuery
            {
                Id = id,
                ClientId = ClientId,
            });

            return Ok(vm);
        }

        /// <summary>
        /// Updates the address by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     PUT /api/1.0/addresstoclient
        ///     {
        ///         "id": "a3eb7b4a-9f4e-4c71-8619-398655c563b8",
        ///         "address": "Sample address",
        ///         "latitude": 0,
        ///         "longitude": 0
        ///     }
        ///     
        /// </remarks>
        /// <param name="updateAddressToClient">UpdateAddressToClientDto object</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="404">Not found</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPut]
        [Authorize]
        [AuthRoleFilter(Roles.Client)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> UpdateAsync([FromBody] UpdateAddressToClientDto updateAddressToClient)
        {
            var command = _mapper.Map<UpdateAddressToClientCommand>(updateAddressToClient, opt =>
            {
                opt.AfterMap((src, dst) =>
                {
                    dst.ClientId = ClientId;
                });
            });

            await Mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Deletes the address by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     DELETE /api/1.0/addresstoclient/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        ///     
        /// </remarks>
        /// <param name="id">AddressToClient id</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="404">Not found</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpDelete("{id}")]
        [AuthRoleFilter(Roles.Client)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            await Mediator.Send(new DeleteAddressToClientCommand
            {
                Id       = id,
                ClientId = ClientId
            });

            return NoContent();
        }
    }
}
 
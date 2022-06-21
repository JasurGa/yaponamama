using System;
using System.Threading.Tasks;
using Atlas.Application.Common.Constants;
using Atlas.Application.CQRS.CardInfoToClients.Commands.CreateCardInfoToClient;
using Atlas.Application.CQRS.CardInfoToClients.Commands.DeleteCardInfoToClient;
using Atlas.Application.CQRS.CardInfoToClients.Commands.UpdateCardInfoToClient;
using Atlas.Application.CQRS.CardInfoToClients.Queries.GetCardInfoToClientDetails;
using Atlas.Application.CQRS.CardInfoToClients.Queries.GetCardInfoToClientList;
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
    public class CardInfoToClientController : BaseController
    {
        private readonly IMapper _mapper;

        public CardInfoToClientController(IMapper mapper) =>
            _mapper = mapper;

        /// <summary>
        /// Creates the card
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /api/1.0/cardinfotoclient
        /// {
        ///     "name": "123",
        ///     "cardNumber": "123",
        ///     "dateOfIssue": "1900-01-01T10:00:00",
        ///     "cvc": "123",
        ///     "cvc2": "123",
        ///     "cardHolder": "123"
        /// }
        /// </remarks>
        /// <param name="createCardInfoToClientDto">CreateCardInfoToClientDto object</param>
        /// <returns>Returns id (guid)</returns> 
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPost]
        [Authorize]
        [AuthRoleFilter(Roles.Client)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateCardInfoToClientDto createCardInfoToClientDto)
        {
            var command = _mapper.Map<CreateCardInfoToClientCommand>(createCardInfoToClientDto, opt =>
                opt.AfterMap((src, dst) => dst.ClientId = ClientId));

            var cardInfoToClientId = await Mediator.Send(command);
            return Ok(cardInfoToClientId);
        }

        /// <summary>
        /// Gets the list of cards
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /api/1.0/cardinfotoclient
        /// </remarks>
        /// <returns>Returns CardInfoToClientListVm object</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpGet]
        [Authorize]
        [AuthRoleFilter(Roles.Client)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<CardInfoToClientListVm>> GetAllAsync()
        {
            var vm = await Mediator.Send(new GetCardInfoToClientListQuery
            {
                ClientId = ClientId,
            });

            return Ok(vm);
        }

        /// <summary>
        /// Gets the card by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /api/1.0/cardinfotoclient/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        /// </remarks>
        /// <param name="id">CardInfoToClient id (guid)</param>
        /// <returns>Returns CardInfoToClientDetailsVm object</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not found</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpGet("{id}")]
        [AuthRoleFilter(Roles.Client)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<CardInfoToClientDetailsVm>> GetAsync(Guid id)
        {
            var vm = await Mediator.Send(new GetCardInfoToClientDetailsQuery
            {
                Id       = id,
                ClientId = ClientId,
            });

            return Ok(vm);
        }

        /// <summary>
        /// Updates the card by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// PUT /api/1.0/cardinfotoclient
        /// {
        ///     "id": "a3eb7b4a-9f4e-4c71-8619-398655c563b8",
        ///     "name": "123",
        ///     "cardNumber": "123",
        ///     "dateOfIssue": "1900-01-01T10:00:00",
        ///     "cvc": "123",
        ///     "cvc2": "123",
        ///     "cardHolder": "123"
        /// }
        /// </remarks>
        /// <param name="updateCardInfoToClient">UpdateCardInfoToClientDto object</param>
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
        public async Task<ActionResult> UpdateAsync([FromBody] UpdateCardInfoToClientDto updateCardInfoToClient)
        {
            var command = _mapper.Map<UpdateCardInfoToClientCommand>(updateCardInfoToClient, opt =>
                opt.AfterMap((src, dst) => dst.ClientId = ClientId ));

            await Mediator.Send(command);
            return Ok();
        }

        /// <summary>
        /// Deletes the card by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE /api/1.0/cardinfotoclient/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        /// </remarks>
        /// <param name="id">CardInfoToClient id</param>
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
            await Mediator.Send(new DeleteCardInfoToClientCommand
            {
                Id       = id,
                ClientId = ClientId
            });

            return Ok();
        }
    }
}

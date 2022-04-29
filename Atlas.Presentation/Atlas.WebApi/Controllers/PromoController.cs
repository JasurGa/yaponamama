using Atlas.Application.CQRS.Promos.Commands.CreatePromo;
using Atlas.Application.CQRS.Promos.Commands.DeletePromo;
using Atlas.Application.CQRS.Promos.Commands.UpdatePromo;
using Atlas.Application.CQRS.Promos.Queries.GetPromoDetails;
using Atlas.Application.CQRS.Promos.Queries.GetPromoList;
using Atlas.Application.CQRS.Promos.Queries.GetPromoPagedList;
using Atlas.Application.Models;
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
    public class PromoController : BaseController
    {
        private readonly IMapper _mapper;

        public PromoController(IMapper mapper) =>
            _mapper = mapper;

        /// <summary>
        /// Get the list of promos
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /api/1.0/promo
        /// </remarks>
        /// <returns>Returns PromoListVm object</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<PromoListVm>> GetAllAsync()
        {
            var vm = await Mediator.Send(new GetPromoListQuery());

            return Ok(vm);
        }

        /// <summary>
        /// Get the paged list of promos
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /api/1.0/promo/paged?pageIndex=0&amp;pageSize=10
        /// </remarks>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>Returns PageDto PromoLookupDto object</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpGet("paged")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<PageDto<PromoLookupDto>>> GetAllPagedAsync([FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 10) 
        {
            var vm = await Mediator.Send(new GetPromoPagedListQuery 
            { 
                PageIndex = pageIndex,
                PageSize  = pageSize,
            });

            return Ok(vm);
        }

        /// <summary>
        /// Get the promo by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /api/1.0/promo/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        /// </remarks>
        /// <param name="id">Promo id (guid)</param>
        /// <returns>Returns PromoDetailsVm object</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not found</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<PromoDetailsVm>> GetAsync(Guid id)
        {
            var vm = await Mediator.Send(new GetPromoDetailsQuery
            {
                Id = id,
            });

            return Ok(vm);
        }

        /// <summary>
        /// Creates a new promo
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /api/1.0/promo
        /// {
        ///     "name": "Sample name",
        ///     "discountPrice": 1000,
        ///     "discountPercent": 10,
        /// }
        /// </remarks>
        /// <param name="createPromoDto">CreatePromoDto object</param>
        /// <returns>Returns id (guid)</returns> 
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Guid>> CreateAsync([FromBody] CreatePromoDto createPromoDto)
        {
            var command = _mapper.Map<CreatePromoCommand>(createPromoDto);

            var promoId = await Mediator.Send(command);

            return Ok(promoId);
        }

        /// <summary>
        /// Updates the promo by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// PUT /api/1.0/promo
        /// {
        ///     "id": "a3eb7b4a-9f4e-4c71-8619-398655c563b8"
        ///     "name": "Sample name",
        ///     "discountPrice": 1000,
        ///     "discountPercent": 10,
        /// }
        /// </remarks>
        /// <param name="updatePromoDto">UpdatePromoDto object</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="404">Not found</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPut]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> UpdateAsync([FromBody] UpdatePromoDto updatePromoDto)
        {
            var command = _mapper.Map<UpdatePromoCommand>(updatePromoDto);

            await Mediator.Send(command);

            return NoContent();
        }

        /// <summary>
        /// Deletes a specific promo by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE /api/1.0/promo/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        /// </remarks>
        /// <param name="id">Promo id</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="404">Not found</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpDelete("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            await Mediator.Send(new DeletePromoCommand
            {
                Id = id,
            });

            return NoContent();
        }

    }
}

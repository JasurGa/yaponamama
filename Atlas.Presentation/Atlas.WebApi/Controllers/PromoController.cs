using Atlas.Application.Common.Constants;
using Atlas.Application.CQRS.Promos.Commands.CreatePromo;
using Atlas.Application.CQRS.Promos.Commands.DeletePromo;
using Atlas.Application.CQRS.Promos.Commands.UpdatePromo;
using Atlas.Application.CQRS.Promos.Queries.GetPromoDetails;
using Atlas.Application.CQRS.Promos.Queries.GetPromoList;
using Atlas.Application.CQRS.Promos.Queries.GetPromoPagedList;
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
    public class PromoController : BaseController
    {
        private readonly IMapper _mapper;

        public PromoController(IMapper mapper) =>
            _mapper = mapper;

        /// <summary>
        /// Gets the list of promo codes
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
        [AuthRoleFilter(Roles.Admin)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<PromoListVm>> GetAllAsync()
        {
            var vm = await Mediator.Send(new GetPromoListQuery());
            return Ok(vm);
        }

        /// <summary>
        /// Gets the paged list of promo codes
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
        [Authorize]
        [HttpGet("paged")]
        [AuthRoleFilter(Roles.Admin)]
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
        /// Gets the promo code by id
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
        [Authorize]
        [HttpGet("{id}")]
        [AuthRoleFilter(Roles.Admin)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<PromoDetailsVm>> GetAsync([FromRoute] Guid id)
        {
            var vm = await Mediator.Send(new GetPromoDetailsQuery
            {
                Id = id,
            });

            return Ok(vm);
        }

        /// <summary>
        /// Creates a new promo code
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /api/1.0/promo
        /// {
        ///     "name": "Sample name",
        ///     "discountPrice": 1000,
        ///     "discountPercent": 10,
        ///     "expiresAt": "1900-01-01T01:01:01"
        /// }
        /// </remarks>
        /// <param name="createPromoDto">CreatePromoDto object</param>
        /// <returns>Returns id (guid)</returns> 
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPost]
        [Authorize]
        [AuthRoleFilter(Roles.Admin)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Guid>> CreateAsync([FromBody] CreatePromoDto createPromoDto)
        {
            var promoId = await Mediator.Send(_mapper.Map<CreatePromoDto,
                CreatePromoCommand>(createPromoDto));

            return Ok(promoId);
        }

        /// <summary>
        /// Updates the promo code by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// PUT /api/1.0/promo
        /// {
        ///     "id": "a3eb7b4a-9f4e-4c71-8619-398655c563b8"
        ///     "name": "Sample name",
        ///     "discountPrice": 1000,
        ///     "discountPercent": 10,
        ///     "expiresAt": "1900-01-01T01:01:01"
        /// }
        /// </remarks>
        /// <param name="updatePromoDto">UpdatePromoDto object</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="404">Not found</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPut]
        [Authorize]
        [AuthRoleFilter(Roles.Admin)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> UpdateAsync([FromBody] UpdatePromoDto updatePromoDto)
        {
            await Mediator.Send(_mapper.Map<UpdatePromoDto,
                UpdatePromoCommand>(updatePromoDto));

            return NoContent();
        }

        /// <summary>
        /// Deletes a specific promo code by id
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
        [Authorize]
        [HttpDelete("{id}")]
        [AuthRoleFilter(Roles.Admin)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            await Mediator.Send(new DeletePromoCommand
            {
                Id = id
            });

            return NoContent();
        }
    }
}

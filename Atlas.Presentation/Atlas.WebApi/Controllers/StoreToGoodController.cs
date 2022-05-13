using Atlas.Application.CQRS.StoreToGoods.Commands.CreateStoreToGood;
using Atlas.Application.CQRS.StoreToGoods.Commands.DeleteStoreToGood;
using Atlas.Application.CQRS.StoreToGoods.Commands.UpdateStoreToGood;
using Atlas.Application.CQRS.StoreToGoods.Queries.GetStoreToGoodByStoreId;
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
    public class StoreToGoodController : BaseController
    {
        private readonly IMapper _mapper;

        public StoreToGoodController(IMapper mapper) =>
            _mapper = mapper;

        /// <summary>
        /// Get StoreToGood relation by store id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /api/1.0/storeToGood/store/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        /// </remarks>
        /// <returns>Returns StoreToGoodVm object</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not found</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<StoreToGoodVm>> GetByStoreIdAsync(Guid storeId)
        {
            var vm = await Mediator.Send(new GetStoreToGoodByStoreIdQuery { 
                StoreId = storeId,
            });

            return Ok(vm);
        }

        /// <summary>
        /// Attaches goods to store
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /api/1.0/storeToGood
        /// {
        ///     "GoodId": "a3eb7b4a-9f4e-4c71-8619-398655c563b8",
        ///     "Store":  "a3eb7b4a-9f4e-4c71-8619-398655c563b8",
        ///     "Count":  10,
        /// }
        /// </remarks>
        /// <param name="createStoreToGood">CreateStoreToGoodDto object</param>
        /// <returns>Returns id (guid)</returns> 
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> CreateAsync([FromBody] CreateStoreToGoodDto createStoreToGood)
        {
            var command = _mapper.Map<CreateStoreToGoodCommand>(createStoreToGood);

            var storeToGoodId = Mediator.Send(command);

            return Ok(storeToGoodId);
        }

        /// <summary>
        /// Updates the good from store relation
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// PUT /api/1.0/storeToGood
        /// {
        ///     "id": "a3eb7b4a-9f4e-4c71-8619-398655c563b8"
        ///     "count": 10,
        /// }
        /// </remarks>
        /// <param name="updateStoreToGoodDto">UpdateStoreToGoodDto object</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="404">Not found</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPut]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> UpdateAsync([FromBody] UpdateStoreToGoodDto updateStoreToGoodDto)
        {
            var command = _mapper.Map<UpdateStoreToGoodCommand>(updateStoreToGoodDto);

            await Mediator.Send(command);

            return NoContent();
        }

        /// <summary>
        /// Deataches goods from store by StoreToGood relation id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE /api/1.0/storeToGood/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        /// </remarks>
        /// <param name="id">StoreToGood id</param>
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
            await Mediator.Send(new DeleteStoreToGoodCommand
            {
                Id = id,
            });

            return NoContent();
        }
    }
}

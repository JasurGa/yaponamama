﻿using Atlas.Application.Common.Constants;
using Atlas.Application.CQRS.StoreToGoods.Commands.CreateStoreToGood;
using Atlas.Application.CQRS.StoreToGoods.Commands.DeleteStoreToGood;
using Atlas.Application.CQRS.StoreToGoods.Commands.UpdateStoreToGood;
using Atlas.Application.CQRS.StoreToGoods.Queries.GetStoreToGoodByStoreId;
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
    public class StoreToGoodController : BaseController
    {
        private readonly IMapper _mapper;

        public StoreToGoodController(IMapper mapper) =>
            _mapper = mapper;

        /// <summary>
        /// Gets good ids by store id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /api/1.0/storetogood/store/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        /// </remarks>
        /// <returns>Returns StoreToGoodVm object</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not found</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpGet("store/{storeId}")]
        [AuthRoleFilter(new string[] { Roles.Admin, Roles.SupplyManager, Roles.Support })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<StoreToGoodVm>> GetByStoreIdAsync([FromRoute] Guid storeId)
        {
            var vm = await Mediator.Send(new GetStoreToGoodByStoreIdQuery
            { 
                StoreId = storeId,
            });

            return Ok(vm);
        }

        /// <summary>
        /// Attaches goods to store
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /api/1.0/storetogood
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
        [AuthRoleFilter(new string[] { Roles.Admin, Roles.SupplyManager, Roles.Support })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> CreateAsync([FromBody] CreateStoreToGoodDto createStoreToGood)
        {
            var storeToGoodId = await Mediator.Send(_mapper
                .Map<CreateStoreToGoodCommand>(createStoreToGood));

            return Ok(storeToGoodId);
        }

        /// <summary>
        /// Updates the good from store relation
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// PUT /api/1.0/storetogood
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
        [AuthRoleFilter(new string[] { Roles.Admin, Roles.SupplyManager, Roles.Support })]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> UpdateAsync([FromBody]
            UpdateStoreToGoodDto updateStoreToGoodDto)
        {
            await Mediator.Send(_mapper.Map<UpdateStoreToGoodDto, UpdateStoreToGoodCommand>
                (updateStoreToGoodDto));

            return NoContent();
        }

        /// <summary>
        /// Detaches good from store
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
        [Authorize]
        [HttpDelete("{id}")]
        [AuthRoleFilter(new string[] { Roles.Admin, Roles.SupplyManager, Roles.Support })]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            await Mediator.Send(new DeleteStoreToGoodCommand
            {
                Id = id
            });

            return NoContent();
        }
    }
}

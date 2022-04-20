﻿using System;
using System.Threading.Tasks;
using Atlas.Application.CQRS.Clients.Queries.GetClientDetails;
using Atlas.Application.CQRS.Users.Queries.GetUserDetails;
using Atlas.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Atlas.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("/api/{version:apiVersion}/[controller]")]
    public class ProfileController : BaseController
    {
        /// <summary>
        /// Gets user profile
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /api/1.0/profile
        /// </remarks>
        /// <returns>Returns UserDetailsVm object</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<UserDetailsVm>> GetUserProfileAsync()
        {
            var vm = await Mediator.Send(new GetUserDetailsQuery
            {
                Id = UserId
            });

            return Ok(vm);
        }

        /// <summary>
        /// Gets client profile
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /api/1.0/profile/client
        /// </remarks>
        /// <returns>Returns ClientDetailsVm object</returns>\
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpGet("client")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ClientDetailsVm>> GetClientProfileAsync()
        {
            var vm = await Mediator.Send(new GetClientDetailsQuery
            {
                Id = ClientId
            });

            return Ok(vm);
        }
    }
}

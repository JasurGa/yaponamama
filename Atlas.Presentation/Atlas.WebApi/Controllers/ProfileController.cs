﻿using System;
using System.Threading.Tasks;
using Atlas.Application.CQRS.Clients.Commands.UpdateClient;
using Atlas.Application.CQRS.Clients.Queries.GetClientDetails;
using Atlas.Application.CQRS.Couriers.Commands.UpdateCourier;
using Atlas.Application.CQRS.Users.Commands.UpdateUser;
using Atlas.Application.CQRS.Users.Queries.GetUserDetails;
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
    public class ProfileController : BaseController
    {

        private readonly IMapper _mapper;

        public ProfileController(IMapper mapper) => _mapper = mapper;

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

        /// <summary>
        /// Updates the user profile
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// PUT /api/1.0/profile
        /// {
        ///     "firstname": "John",
        ///     "lastname": "Doe",
        ///     "birthDay": "1900-01-01T10:00:00",
        ///     "avatarPhotoPath": ""
        /// }
        /// </remarks>
        /// <param name="updateUser">UpdateUserDto object</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="404">Not found</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPut]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> UpdateUserProfileAsync([FromBody] UpdateUserDto updateUser)
        {
            var command = _mapper.Map<UpdateUserCommand>(updateUser, opt =>
            {
                opt.AfterMap((src, dst) =>
                {
                    dst.Id = UserId;
                });
            });

            await Mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Updates the client profile
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// PUT /api/1.0/profile/client
        /// {
        ///     "passportPhotoPath": "test",
        ///     "selfieWithPassportPhotoPath": "test"
        /// }
        /// </remarks>
        /// <param name="updateClient">UpdateClientDto object</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="404">Not found</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPut("client")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> UpdateClientProfileAsync([FromBody] UpdateClientDto updateClient)
        {
            var command = _mapper.Map<UpdateClientCommand>(updateClient, opt =>
            {
                opt.AfterMap((src, dst) =>
                {
                    dst.Id = ClientId;
                });
            });

            await Mediator.Send(command);

            return NoContent();
        }

        /// <summary>
        /// Updates the courier profile
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// PUT /api/1.0/profile/courier
        /// {
        ///     "passportPhotoPath": "test",
        ///     "driverLicensePath": "test"
        /// }
        /// </remarks>
        /// <param name="updateCourier">UpdateCourierDto object</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="404">Not found</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPut("courier")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> UpdateCourierProfileAsync([FromBody] UpdateCourierDto updateCourier)
        {
            var command = _mapper.Map<UpdateCourierCommand>(updateCourier, opt =>
            {
                opt.AfterMap((src, dst) =>
                {
                    dst.Id = CourierId;
                });
            });

            await Mediator.Send(command);
            return NoContent();
        }
    }
}

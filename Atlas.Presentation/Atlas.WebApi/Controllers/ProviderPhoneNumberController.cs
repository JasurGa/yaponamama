using Atlas.Application.Common.Constants;
using Atlas.Application.CQRS.ProviderPhoneNumbers.Commands.CreateProviderPhoneNumber;
using Atlas.Application.CQRS.ProviderPhoneNumbers.Commands.DeleteProviderPhoneNumber;
using Atlas.Application.CQRS.ProviderPhoneNumbers.Commands.UpdateProviderPhoneNumber;
using Atlas.Application.CQRS.ProviderPhoneNumbers.Queries.GetProviderPhoneNumberDetails;
using Atlas.Application.CQRS.ProviderPhoneNumbers.Queries.GetProviderPhoneNumberListByProviderId;
using Atlas.Application.CQRS.ProviderPhoneNumbers.Queries.GetProviderPhoneNumberPagedList;
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
    public class ProviderPhoneNumberController : BaseController
    {
        private readonly IMapper _mapper;

        public ProviderPhoneNumberController(IMapper mapper) =>
            _mapper = mapper;

        /// <summary>
        /// Gets the list of phone numbers of providers by provider id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /api/1.0/providerphonenumber/provider/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        /// </remarks>
        /// <param name="providerId">Provider id (guid)</param>
        /// <returns>Returns ProviderPhoneNumberListVm object</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not found</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpGet("provider/{providerId}")]
        [Authorize]
        [AuthRoleFilter(new string[] { Roles.Admin, Roles.SupplyManager, Roles.Support })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ProviderPhoneNumberListVm>> GetAllByProviderIdAsync([FromRoute] Guid providerId)
        {
            var vm = await Mediator.Send(new GetProviderPhoneNumberListByProviderIdQuery
            {
                ProviderId = providerId,
            });

            return Ok(vm);
        }

        /// <summary>
        /// Gets the paged list of phone numbers of providers
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /api/1.0/providerphonenumber/paged?pageIndex=0&amp;pageSize=10
        /// </remarks>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>Returns PageDto ProviderPhoneNumberLookupDto object</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpGet("paged")]
        [Authorize]
        [AuthRoleFilter(new string[] { Roles.Admin, Roles.SupplyManager, Roles.Support })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<PageDto<ProviderPhoneNumberLookupDto>>> GetAllPagedAsync([FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 10)
        {
            var vm = await Mediator.Send(new GetProviderPhoneNumberPagedListQuery
            {
                PageIndex = pageIndex,
                PageSize  = pageSize
            });

            return Ok(vm);
        }

        /// <summary>
        /// Gets the phone number of provider by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /api/1.0/providerphonenumber/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        /// </remarks>
        /// <param name="id">ProviderPhoneNumber id (guid)</param>
        /// <returns>Returns ProviderPhoneNumberDetailsVm object</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not found</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpGet("{id}")]
        [Authorize]
        [AuthRoleFilter(new string[] { Roles.Admin, Roles.SupplyManager, Roles.Support })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ProviderPhoneNumberDetailsVm>> GetAsync([FromRoute] Guid id)
        {
            var vm = await Mediator.Send(new GetProviderPhoneNumberDetailsQuery
            {
                Id = id
            });

            return Ok(vm);
        }

        /// <summary>
        /// Creates a new phone number of provider
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /api/1.0/providerphonenumber
        /// {
        ///     "providerId": "a3eb7b4a-9f4e-4c71-8619-398655c563b8",
        ///     "phoneNumber": "+998901234567"
        /// }
        /// </remarks>
        /// <param name="createProviderPhoneNumber">CreateProviderPhoneNumberDto object</param>
        /// <returns>Returns id (guid)</returns> 
        /// <response code="200">Success</response>
        /// <response code="404">Not found</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPost]
        [Authorize]
        [AuthRoleFilter(new string[] { Roles.Admin, Roles.SupplyManager, Roles.Support })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Guid>> CreateAsync([FromBody] CreateProviderPhoneNumberDto createProviderPhoneNumber)
        {
            var providerPhoneNumberId = await Mediator.Send(_mapper.Map<CreateProviderPhoneNumberDto,
                CreateProviderPhoneNumberCommand>(createProviderPhoneNumber));

            return Ok(providerPhoneNumberId);
        }

        /// <summary>
        /// Updates the phone number of provider
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// PUT /api/1.0/providerphonenumber
        /// {
        ///     "id": "a3eb7b4a-9f4e-4c71-8619-398655c563b8",
        ///     "providerId": "a3eb7b4a-9f4e-4c71-8619-398655c563b8",
        ///     "phoneNumber": "+998901234567",
        /// }
        /// </remarks>
        /// <param name="updateProviderPhoneNumber">UpdateProviderPhoneNumberDto object</param>
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
        public async Task<ActionResult> UpdateAsync([FromBody] UpdateProviderPhoneNumberDto updateProviderPhoneNumber)
        {
            await Mediator.Send(_mapper.Map<UpdateProviderPhoneNumberDto,
                UpdateProviderPhoneNumberCommand>(updateProviderPhoneNumber));

            return NoContent();
        }

        /// <summary>
        /// Deletes a specific providers phone number
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE /api/1.0/providerphonenumber/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        /// </remarks>
        /// <param name="id">ProviderPhoneNumber id</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="404">Not found</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpDelete("{id}")]
        [Authorize]
        [AuthRoleFilter(new string[] { Roles.Admin, Roles.SupplyManager, Roles.Support })]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            await Mediator.Send(new DeleteProviderPhoneNumberCommand
            {
                Id = id,
            });

            return NoContent();
        }
    }
}

using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Atlas.WebApi.Models;
using Atlas.Application.CQRS.VerificationRequests.Commands.CreateVerificationRequest;
using Microsoft.AspNetCore.Authorization;
using Atlas.WebApi.Filters;
using Atlas.Application.Common.Constants;
using Atlas.Application.CQRS.VerificationRequests.Commands.AcceptVerificationRequest;
using Atlas.Application.CQRS.VerificationRequests.Commands.DeclineVerificationRequest;
using Atlas.Application.CQRS.VerificationRequests.Queries.GetMyVerificationRequests;
using Atlas.Application.CQRS.VerificationRequests.Queries.GetPagedVerificationRequestsList;
using Atlas.Application.CQRS.VerificationRequests.Queries.GetVerificationRequestDetails;
using Atlas.Application.CQRS.VerificationRequests.Queries.GetMyVerificationRequestDetails;
using Atlas.Application.Models;

namespace Atlas.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("/api/{version:apiVersion}/[controller]")]
    public class VerificationRequestController : BaseController
    {
        private readonly IMapper _mapper;

        public VerificationRequestController(IMapper mapper) =>
            _mapper = mapper;

        /// <summary>
        /// Create verification request
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/1.0/verificationrequest
        ///     {
        ///         "passportPhotoPath": "0123456789abcdef.png",
        ///         "selfieWithPassportPhotoPath": "0123456789abcdef.png",
        ///     }
        ///     
        /// </remarks>
        /// <param name="createVerificationRequest">CreateVerificationRequestDto object</param>
        /// <returns>Returns Verification Request Id (Guid)</returns>
        /// <response code="200">Success</response>
        /// <response code="404">NotFound</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPost]
        [Authorize]
        [AuthRoleFilter(Roles.Client)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Guid>> CreateAsync(CreateVerificationRequestDto createVerificationRequest)
        {
            var vm = await Mediator.Send(_mapper.Map<CreateVerificationRequestDto,
                CreateVerificationRequestCommand>(createVerificationRequest, opt =>
                {
                    opt.AfterMap((src, dst) =>
                    {
                        dst.ClientId = ClientId;
                    });
                }));

            return Ok(vm);
        }

        /// <summary>
        /// Accept verification request
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PATCH /api/1.0/verificationrequest/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        ///     
        /// </remarks>
        /// <param name="id">Verification request id (Guid)</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="404">NotFound</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpPatch("{id}")]
        [AuthRoleFilter(new string[] { Roles.Admin, Roles.HeadRecruiter, Roles.SupplyManager, Roles.Support })]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> AcceptAsync([FromRoute] Guid id)
        {
            await Mediator.Send(new AcceptVerificationRequestCommand
            {
                Id = id
            });

            return NoContent();
        }

        /// <summary>
        /// Decline verification request
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /api/1.0/verificationrequest/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        ///     "comment"
        ///     
        /// </remarks>
        /// <param name="id">Verification request id (Guid)</param>
        /// <param name="comment">Comment (string)</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="404">NotFound</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpDelete("{id}")]
        [AuthRoleFilter(new string[] { Roles.Admin, Roles.HeadRecruiter, Roles.SupplyManager, Roles.Support })]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> DeclineAsync([FromRoute] Guid id, [FromBody] string comment)
        {
            await Mediator.Send(new DeclineVerificationRequestCommand
            {
                Id      = id,
                Comment = comment
            });

            return NoContent();
        }

        /// <summary>
        /// Get my verification requests 
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/1.0/verificationrequest/my?pageIndex=0&amp;pageSize=10
        /// 
        /// </remarks>
        /// <returns>Returns PageDto VerificationRequestLookupDto</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpGet("my")]
        [AuthRoleFilter(Roles.Client)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<PageDto<VerificationRequestLookupDto>>> GetMyVerificationRequestsAsync([FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 10)
        {
            var vm = await Mediator.Send(new GetMyVerificationRequestsQuery
            {
                ClientId  = ClientId,
                PageSize  = pageSize,
                PageIndex = pageIndex
            });

            return Ok(vm);
        }

        /// <summary>
        /// Get verification requests 
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/1.0/verificationrequest?pageIndex=0&amp;pageSize=10
        /// 
        /// </remarks>
        /// <returns>Returns PageDto VerificationRequestLookupDto</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpGet]
        [Authorize]
        [AuthRoleFilter(new string[] { Roles.Admin, Roles.HeadRecruiter, Roles.SupplyManager, Roles.Support })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<PageDto<VerificationRequestLookupDto>>> GetVerificationRequestsAsync([FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 10)
        {
            var vm = await Mediator.Send(new GetPagedVerificationRequestsListQuery
            {
                PageSize  = pageSize,
                PageIndex = pageIndex
            });

            return Ok(vm);
        }

        /// <summary>
        /// Get verification request by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/1.0/verificationrequest/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        /// 
        /// </remarks>
        /// <returns>Returns VerificationRequestDetailsVm</returns>
        /// <response code="200">Success</response>
        /// <response code="404">NotFound</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpGet("{id}")]
        [AuthRoleFilter(new string[] { Roles.Admin, Roles.HeadRecruiter, Roles.SupplyManager, Roles.Support })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<VerificationRequestDetailsVm>> GetByIdAsync([FromRoute] Guid id)
        {
            var vm = await Mediator.Send(new GetVerificationRequestDetailsQuery
            {
                Id = id
            });

            return Ok(vm);
        }

        /// <summary>
        /// Get my verification request by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/1.0/verificationrequest/my/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        /// 
        /// </remarks>
        /// <returns>Returns VerificationRequestDetailsVm</returns>
        /// <response code="200">Success</response>
        /// <response code="404">NotFound</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpGet("my/{id}")]
        [AuthRoleFilter(Roles.Client)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<VerificationRequestDetailsVm>> GetMyByIdAsync([FromRoute] Guid id)
        {
            var vm = await Mediator.Send(new GetMyVerificationRequestDetailsQuery
            {
                Id       = id,
                ClientId = ClientId
            });

            return Ok(vm);
        }
    }
}


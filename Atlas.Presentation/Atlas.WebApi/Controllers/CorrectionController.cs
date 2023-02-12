using System;
using AutoMapper;
using Atlas.WebApi.Models;
using System.Threading.Tasks;
using Atlas.Application.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Atlas.Application.CQRS.Corrections.Commands.CreateCorrection;
using Atlas.Application.CQRS.Corrections.Queries.GetCorrectionDetails;
using Atlas.Application.CQRS.Corrections.Queries.GetCorrectionList;
using Atlas.Application.CQRS.Corrections.Queries.GetCorrectionPagedList;
using Atlas.WebApi.Filters;
using Atlas.Application.Common.Constants;
using Atlas.Application.CQRS.Corrections.Commands.UpdateCorrectionCausedBy;
using Atlas.Application.CQRS.Stores.Commands.UpdateStore;

namespace Atlas.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("/api/{version:apiVersion}/[controller]")]
    public class CorrectionController : BaseController
    {
        private readonly IMapper _mapper;

        public CorrectionController(IMapper mapper) =>
            _mapper = mapper;

        /// <summary>
        /// Gets the list of corrections
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/1.0/correction
        /// 
        /// </remarks>
        /// <returns>Returns CorrectionListVm object</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpGet]
        [Authorize]
        [AuthRoleFilter(new string[] { Roles.Admin, Roles.SupplyManager })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<CorrectionListVm>> GetAllAsync()
        {
            var vm = await Mediator.Send(new GetCorrectionListQuery());
            return Ok(vm);
        }

        /// <summary>
        /// Get the paged list of corrections
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/1.0/correction/paged?pageIndex=0&amp;pageSize=10&amp;showDeleted=false&amp;sortable=Name&amp;ascending=true
        /// 
        /// </remarks>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="showDeleted">Show deleted (bool)</param>
        /// <param name="sortable">Property to sort by</param>
        /// <param name="ascending">Order: Ascending (true) || Descending (false)</param>
        /// <param name="filterCategoryId">Filtering correction goods by category</param>
        /// <returns>Returns PageDto CorrectionLookupDto object</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpGet("paged")]
        [AuthRoleFilter(new string[] { Roles.Admin, Roles.SupplyManager })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<PageDto<CorrectionLookupDto>>> GetAllPagedAsync(
            [FromQuery] int    pageIndex        = 0,
            [FromQuery] int    pageSize         = 10,
            [FromQuery] bool   showDeleted      = false,
            [FromQuery] string sortable         = "Id",
            [FromQuery] bool   ascending        = true,
            [FromQuery] Guid?  filterCategoryId = null)
        {
            var vm = await Mediator.Send(new GetCorrectionPagedListQuery
            {
                PageIndex        = pageIndex,
                PageSize         = pageSize,
                ShowDeleted      = showDeleted,
                Sortable         = sortable,
                Ascending        = ascending,
                FilterCategoryId = filterCategoryId
            });

            return Ok(vm);
        }

        /// <summary>
        /// Gets the specific correction details
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/1.0/correction/a3eb7b4a-9f4e-4c71-8619-398655c563b8
        /// 
        /// </remarks>
        /// <param name="id">Correction id (guid)</param>
        /// <returns>Returns CorrectionDetailsVm object</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not found</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpGet("{id}")]
        [AuthRoleFilter(new string[] { Roles.Admin, Roles.SupplyManager })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<CorrectionDetailsVm>> GetAsync(Guid id)
        {
            var vm = await Mediator.Send(new GetCorrectionDetailsQuery
            {
                Id = id
            });

            return Ok(vm);
        }

        /// <summary>
        /// Creates a new correction
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     POST /api/1.0/correction
        ///     {
        ///         "goodId": "a3eb7b4a-9f4e-4c71-8619-398655c563b8",
        ///         "storeId": "a3eb7b4a-9f4e-4c71-8619-398655c563b8",
        ///         "causeBy": "Test cause",
        ///         "count": 10
        ///     }
        ///     
        /// </remarks>
        /// <param name="createCorrection">CreateCorrectionDto object</param>
        /// <returns>Returns id (guid)</returns> 
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPost]
        [Authorize]
        [AuthRoleFilter(new string[] { Roles.Admin, Roles.SupplyManager })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Guid>> CreateAsync([FromBody] CreateCorrectionDto createCorrection)
        {
            var correctionId = await Mediator.Send(_mapper.Map<CreateCorrectionDto,
                CreateCorrectionCommand>(createCorrection, opt =>
                {
                    opt.AfterMap((src, dst) =>
                    {
                        dst.UserId = UserId;
                    });
                }));

            return Ok(correctionId);
        }

        /// <summary>
        /// Updates a correction's causedBy field (correction type)
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     PUT /api/1.0/correction/causedBy
        ///     {
        ///         "Id": "a3eb7b4a-9f4e-4c71-8619-398655c563b8",
        ///         "causeBy": "Test cause",
        ///     }
        ///     
        /// </remarks>
        /// <param name="updateCorrectionCausedBy">UpdateCorrectionCausedByDto object</param>
        /// <returns>Returns NoContent</returns> 
        /// <response code="200">Success</response>
        /// <response code="404">Not found</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPut("causedBy")]
        [Authorize]
        [AuthRoleFilter(new string[] { Roles.Admin, Roles.SupplyManager })]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> UpdateCausedByAsync([FromBody] UpdateCorrectionCausedByDto updateCorrectionCausedBy)
        {
            await Mediator.Send(_mapper.Map<UpdateCorrectionCausedByDto,
                UpdateCorrectionCausedByCommand>(updateCorrectionCausedBy));

            return NoContent();
        }
    }
}

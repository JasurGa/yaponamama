using System;
using System.Threading.Tasks;
using Atlas.Application.CQRS.ChatMessages.Commands.CreateChatMessage;
using Atlas.Application.CQRS.ChatMessages.Queries.GetChatMessagesForUser;
using Atlas.Application.CQRS.ChatMessages.Queries.GetChatUsers;
using Atlas.Application.Models;
using Atlas.WebApi.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Atlas.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("/api/{version:apiVerison}/[controller]")]
    public class ChatController : BaseController
    {
        private readonly IMapper _mapper;

        public ChatController(IMapper mapper) =>
            _mapper = mapper;

        /// <summary>
        /// Creates the chat message for user
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /api/1.0/chat
        /// {
        ///     "body": "Hello, world!",
        ///     "optional": "",
        ///     "messageType": 0,
        ///     "toUserId": "a3eb7b4a-9f4e-4c71-8619-398655c563b8"
        /// }
        /// </remarks>
        /// <param name="createChatMessageDto">CreateChatMessageDto createChatMessageDto object</param>
        /// <returns>Returns id (Guid)</returns>
        /// <response code="200">Success</response>
        /// <response code="404">NotFound</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Guid>> CreateAsync([FromBody] CreateChatMessageDto createChatMessageDto)
        {
            var vm = await Mediator.Send(_mapper.Map<CreateChatMessageDto,
                CreateChatMessageCommand>(createChatMessageDto, opt =>
                {
                    opt.AfterMap((src, dst) =>
                    {
                        dst.FromUserId = UserId;
                    });
                }));

            return Ok(vm);
        }

        /// <summary>
        /// Get chat users for user
        /// </summary>
        /// <remarks>
        /// GET /api/1.0/chat/users
        /// </remarks>
        /// <returns>Returns ChatUsersListVm</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpGet("users")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ChatUsersListVm>> GetChatUsersAsync()
        {
            var vm = await Mediator.Send(new GetChatUsersQuery
            {
                UserId = UserId
            });

            return Ok(vm);
        }

        /// <summary>
        /// Get chat messages for chat
        /// </summary>
        /// <remarks>
        /// GET /api/1.0/chat/a3eb7b4a-9f4e-4c71-8619-398655c563b8?pageIndex=0&amp;pageSize=20
        /// </remarks>
        /// <returns>Returns PageDto ChatMessageLookupDto</returns>
        /// <response code="200">Success</response>
        /// <response code="404">NotFound</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<PageDto<ChatMessageLookupDto>>> GetChatMessagesAsync([FromRoute] Guid id, [FromQuery] int pageIndex = 0,
            [FromQuery] int pageSize = 20)
        {
            var vm = await Mediator.Send(new GetChatMessagesForUserQuery
            {
                ChatUserId = id,
                MyUserId   = UserId,
                PageSize   = pageSize,
                PageIndex  = pageIndex,
            });

            return Ok(vm);
        }
    }
}

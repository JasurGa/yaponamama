using System;
using MediatR;

namespace Atlas.Application.CQRS.Users.Queries.GetUserDetails
{
    public class GetUserDetailsQuery : IRequest<UserDetailsVm>
    {
        public Guid Id { get; set; }
    }
}

using System;
using MediatR;

namespace Atlas.Application.CQRS.Statistics.Queries.GetNumberOfRegistrationsOfUsers
{
    public class GetNumberOfRegistrationsOfUsersQuery : IRequest<long>
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}

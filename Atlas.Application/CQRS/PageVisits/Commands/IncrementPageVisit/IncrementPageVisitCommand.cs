using System;
using MediatR;

namespace Atlas.Application.CQRS.PageVisits.Commands.IncrementPageVisit
{
    public class IncrementPageVisitCommand : IRequest<int>
    {
        public string Path { get; set; }
    }
}

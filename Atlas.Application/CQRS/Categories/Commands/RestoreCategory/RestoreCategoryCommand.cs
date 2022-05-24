using System;
using MediatR;

namespace Atlas.Application.CQRS.Categories.Commands.RestoreCategory
{
    public class RestoreCategoryCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}

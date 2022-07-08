using System;
using MediatR;

namespace Atlas.Application.CQRS.Categories.Commands.RemoveCategoryParent
{
    public class RemoveCategoryParentCommand : IRequest
    {
        public Guid CategoryId { get; set; }

        public Guid ParentId { get; set; }
    }
}

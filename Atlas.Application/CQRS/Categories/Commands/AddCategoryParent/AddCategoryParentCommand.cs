using System;
using MediatR;

namespace Atlas.Application.CQRS.Categories.Commands.AddCategoryParent
{
    public class AddCategoryParentCommand : IRequest
    {
        public Guid CategoryId { get; set; }

        public Guid ParentId { get; set; }
    }
}

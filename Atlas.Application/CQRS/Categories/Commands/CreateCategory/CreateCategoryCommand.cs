using System;
using MediatR;

namespace Atlas.Application.CQRS.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommand : IRequest<Guid>
    {
        public string Name { get; set; }

        public bool IsMainCategory { get; set; }
    }
}

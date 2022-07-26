using System;
using MediatR;

namespace Atlas.Application.CQRS.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommand : IRequest
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public bool IsMainCategory { get; set; }
    }
}

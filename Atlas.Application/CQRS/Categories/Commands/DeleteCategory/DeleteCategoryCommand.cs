using MediatR;
using System;
namespace Atlas.Application.CQRS.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}

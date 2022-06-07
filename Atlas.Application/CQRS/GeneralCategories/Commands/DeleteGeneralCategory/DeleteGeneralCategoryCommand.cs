using System;
using MediatR;

namespace Atlas.Application.CQRS.GeneralCategories.Commands.DeleteGeneralCategory
{
    public class DeleteGeneralCategoryCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}

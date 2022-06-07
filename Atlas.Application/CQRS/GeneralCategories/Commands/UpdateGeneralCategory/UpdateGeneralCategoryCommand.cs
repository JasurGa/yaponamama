using System;
using MediatR;

namespace Atlas.Application.CQRS.GeneralCategories.Commands.UpdateGeneralCategory
{
    public class UpdateGeneralCategoryCommand : IRequest
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}

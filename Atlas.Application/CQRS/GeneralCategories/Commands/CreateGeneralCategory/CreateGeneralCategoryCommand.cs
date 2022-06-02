using System;
using MediatR;

namespace Atlas.Application.CQRS.GeneralCategories.Commands.CreateGeneralCategory
{
    public class CreateGeneralCategoryCommand : IRequest<Guid>
    {
        public string Name { get; set; }
    }
}

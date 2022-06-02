using System;
using MediatR;

namespace Atlas.Application.CQRS.GeneralCategories.Commands.RestoreGeneralCategory
{
    public class RestoreGeneralCategoryCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}

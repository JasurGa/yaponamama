﻿using System;
using Atlas.Application.CQRS.Categories.Queries.GetCategoryList;
using MediatR;

namespace Atlas.Application.CQRS.Categories.Queries.GetCategoryParents
{
    public class GetCategoryParentsQuery : IRequest<CategoryListVm>
    {
        public Guid Id { get; set; }

        public bool ShowDeleted { get; set; }

        public bool ShowHidden { get; set; }
    }
}

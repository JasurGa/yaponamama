﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Atlas.Application.CQRS.Providers.Queries.GetProviderList
{
    public class GetProviderListQueryValidator : AbstractValidator<GetProviderListQuery>
    {
        public GetProviderListQueryValidator()
        {
            
        }
    }
}
